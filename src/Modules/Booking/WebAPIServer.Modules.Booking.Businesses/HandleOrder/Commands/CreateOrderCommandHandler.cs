using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OneOf;
using RabbitMQ.Client;
using System.Text;
using WebAPIServer.Modules.Booking.Businesses.Contracts;
using WebAPIServer.Modules.Booking.Businesses.Contracts.Apis;
using WebAPIServer.Modules.Booking.Businesses.Dtos;
using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models;
using WebAPIServer.Modules.Booking.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Booking.Businesses.HandleOrder.Commands
{
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OneOf<Guid, ResponseException>>
	{
		private readonly ILogger<CreateOrderCommandHandler> _logger;
		private readonly IOrderRepository _orderRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IValidator<OrderForCreateDto> _validator;
		private readonly IMapper _mapper;
		private readonly ICatalogModuleApi _catalogModuleApi;
		private readonly IMovieManagementModuleApi _movieManagementModuleApi;
		private readonly ITicketsModuleApi _ticketsModuleApi;
		public CreateOrderCommandHandler(ILogger<CreateOrderCommandHandler> logger,
			IOrderRepository orderRepository,
			IUnitOfWork unitOfWork,
			IValidator<OrderForCreateDto> validator,
			IMapper mapper,
			ICatalogModuleApi productApi,
			IMovieManagementModuleApi movieManagementModuleApi,
			ITicketsModuleApi ticketsModuleApi)
		{
			_logger = logger;
			_orderRepository = orderRepository;
			_unitOfWork = unitOfWork;
			_validator = validator;
			_mapper = mapper;
			_catalogModuleApi = productApi;
			_movieManagementModuleApi = movieManagementModuleApi;
			_ticketsModuleApi = ticketsModuleApi;
		}
		public async Task<OneOf<Guid, ResponseException>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var validationResult = await _validator.ValidateAsync(request.Model);
				if (!validationResult.IsValid)
				{
					return ResponseExceptionHelper.ErrorResponse<Order>(ErrorCode.CreateError, validationResult.Errors);
				}
				var order = _mapper.Map<Order>(request.Model);
				// maping ManagementMovie.Show to Booking.ShowDto
				var show = await _movieManagementModuleApi.GetShowByIdAsync(order.ShowId);
				if (show == null)
				{
					return ResponseExceptionHelper.ErrorResponse<ShowDto>(ErrorCode.NotFound);
				}
				order.CreatedAt = DateTime.UtcNow;
				order.CinemaName = show.CinemaName;
				order.HallName = show.HallName;
				order.MovieTitle = show.MovieTitle;
				order.ShowEndAt = show.EndTime;
				order.ShowStartAt = show.StartTime;
				order.ShowStartEndTime = $"{show.StartTime} - {show.EndTime}";
				
				// maping tickets.ticketType to TicketTypeDto
				var ticketType = await _ticketsModuleApi.GetTicketTypeById(order.TicketTypeId);
				if (ticketType == null)
				{
					return ResponseExceptionHelper.ErrorResponse<TicketTypeDto>(ErrorCode.NotFound);
				}
				order.TicketTypeName = ticketType.Name;
				order.TicketTypePrice = ticketType.Price;

				// maping showSeat to ShowSeatDto
				var hall = await _movieManagementModuleApi.GetHallByIdAsync(show.CinemaHallId);
				if (hall == null)
				{
					return ResponseExceptionHelper.ErrorResponse<HallDto>(ErrorCode.NotFound);
				}
				foreach (var showSeatDto in order.ShowSeats)
				{
					var seat = hall.Seats.FirstOrDefault(x => x.Id == showSeatDto.SeatId);
					if (seat == null)
					{
						return ResponseExceptionHelper.ErrorResponse<OrderShowSeat>(ErrorCode.NotFound);
					}
					//TODO: xử lí thêm race condition ở đây
					if (showSeatDto.Id == seat.Id & showSeatDto.IsReseved)
					{
						return ResponseExceptionHelper.ErrorResponse<OrderShowSeat>(ErrorCode.SeatReserved);
					}
					showSeatDto.ShowId = show.Id;
					showSeatDto.SeatId = seat.Id;
					showSeatDto.SeatPosition = seat.SeatPosition;
					showSeatDto.SeatTypeName = seat.SeatTypeName;
					showSeatDto.SeatTypePrice = seat.SeatTypePrice + order.TicketTypePrice;
				}
				// maping catalog.product to ProductDto
				foreach (var productDto in order.Products)
				{
					var product = await _catalogModuleApi.GetProductByIdAsync(productDto.ProductId);
					if (product == null)
					{
						return ResponseExceptionHelper.ErrorResponse<ProductDto>(ErrorCode.NotFound);
					}
					productDto.ProductName = product.Name;
					productDto.UnitPrice = product.Price;
					productDto.TotalPrice = productDto.UnitPrice * productDto.Quantity;
				}

				// maping catalog.Combo to comboDto
				foreach (var comboDto in order.Combos)
				{
					var combo = await _catalogModuleApi.GetComboByIdAsync(comboDto.ComboId);
					if (combo == null)
					{
						return ResponseExceptionHelper.ErrorResponse<ComboDto>(ErrorCode.NotFound);
					}
					comboDto.ComboName = combo.Name;
					comboDto.UnitPrice = combo.Price;
					comboDto.TotalPrice = comboDto.UnitPrice * comboDto.Quantity;
				}

				// handle sunAmount
				var subAmount = order.Combos.Count > 0 ? order.Combos.Sum(x => x.TotalPrice) : 0;
				subAmount += order.Products.Count > 0 ? order.Products.Sum(x => x.TotalPrice) : 0;
				subAmount += order.ShowSeats.Count > 0 ? order.ShowSeats.Sum(x => x.SeatTypePrice) : 0;

				order.Amount = subAmount;
				order.SubAmount = subAmount;
				order.OrderStatusId = OrderStatusConstants.Requested;
				order.OrderStatus = await _orderRepository.GetStatusByIdAsync(OrderStatusConstants.Requested);
				
				var isOrderSusccessed = await _orderRepository.CreateAsync(order);
				if (isOrderSusccessed)
				{
					await _unitOfWork.SaveChangesAsync();
					SendOrderCreatedMessage(order);
					return order.Id;
				}
				return ResponseExceptionHelper.ErrorResponse<Order>(ErrorCode.OperationFailed);

			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return ResponseExceptionHelper.ErrorResponse<Order>(ErrorCode.OperationFailed);
			}
		}

		private void SendOrderCreatedMessage(Order order)
		{
			const string ExchangeName = "order_exchange";
			const string QueueName = "order_created";
			const string DeadLetterExchange = "order_dead_letter_exchange";
			const string DeadLetterQueue = "order_dead_letter_queue";

			var factory = new ConnectionFactory()
			{
				HostName = "localhost",
				UserName = "guest",
				Password = "guest"
			};

			using var connection = factory.CreateConnection();

			using var channel = connection.CreateModel();


			var mainQueueArguments = new Dictionary<string, object>
			{
				{ "x-message-ttl", 60000 }, // 15 minutes in milliseconds
				{ "x-dead-letter-exchange", DeadLetterExchange } // specify dead-letter exchange
			};

			channel.ExchangeDeclare(
				exchange: ExchangeName,
				type: ExchangeType.Direct,
				durable: true
			);

			var queue = channel.QueueDeclare(
				queue: QueueName,
				durable: false,
				exclusive: false,
				autoDelete: false,
				arguments: mainQueueArguments
			);

			channel.QueueBind(
				queue: QueueName,
				exchange: ExchangeName,
				routingKey: QueueName
			);

			// Declare the dead-letter exchange and queue
			channel.ExchangeDeclare(
				exchange: DeadLetterExchange,
				type: ExchangeType.Fanout,
				durable: true
			);

			channel.QueueDeclare(
				queue: DeadLetterQueue,
				durable: true,
				exclusive: false,
				autoDelete: false
			);

			channel.QueueBind(
				queue: DeadLetterQueue,
				exchange: DeadLetterExchange,
				routingKey: ""
			);


			// Gửi thông điệp đến exchange
			var message = JsonConvert.SerializeObject(order, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});

			var body = Encoding.UTF8.GetBytes(message);

			// Cấu hình các thuộc tính cơ bản (nếu cần)
			var properties = channel.CreateBasicProperties();
			properties.ContentType = "application/json";
			properties.DeliveryMode = 2;

			channel.BasicPublish(
				exchange: ExchangeName,
				routingKey: QueueName,
				basicProperties: properties,
				body: body
			);
		}
	}
}
