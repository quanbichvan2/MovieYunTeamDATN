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
		private readonly IOrderComboRepository _orderProductRepository;
		private readonly IOrderMovieRepository _orderMovieRepository;
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
            ITicketsModuleApi ticketsModuleApi,
            IOrderComboRepository orderProductRepository,
            IOrderMovieRepository orderMovieRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
            _catalogModuleApi = productApi;
            _movieManagementModuleApi = movieManagementModuleApi;
            _ticketsModuleApi = ticketsModuleApi;
            _orderProductRepository = orderProductRepository;
            _orderMovieRepository = orderMovieRepository;
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

                //update on redis

                var order = new Order();
                order.CreatedAt = DateTime.UtcNow;
                //public Guid? UserId { get; set; }
                //public string? UserName { get; set; }
                //public Guid? VoucherId { get; set; }
                order.PaymentId = request.Model.PaymentId;
                order.StatusId = OrderStatusConstants.Confirmed;


				foreach (var item in request.Model?.Line)
				{
					var show = await _movieManagementModuleApi.GetShowByIdAsync(item.ShowId);
					if (show == null)
					{
						return ResponseExceptionHelper.ErrorResponse<ShowDto>(ErrorCode.NotFound);
					}

					var ticketType = await _ticketsModuleApi.GetTicketTypeById(item.TypeId);
					if (ticketType == null)
					{
						return ResponseExceptionHelper.ErrorResponse<TicketTypeDto>(ErrorCode.NotFound);
					}

					var hall = await _movieManagementModuleApi.GetHallByIdAsync(show.CinemaHallId);
					if (hall == null)
					{
						return ResponseExceptionHelper.ErrorResponse<HallDto>(ErrorCode.NotFound);
					}
					var seat = hall.Seats.Where(x => x.Id == item.SeatId).FirstOrDefault();


					var line = new OrderMovie();
					line.OrderId = order.Id;
					line.ShowId = show.Id;
					line.Price = ticketType.Price + seat.SeatTypePrice;
					line.SeatId = seat.Id;
					line.SeatName = seat.SeatPosition;
					line.TypeId = ticketType.Id;
					line.TypeName = ticketType.Name;
					line.HallId = show.CinemaHallId;
					line.HallName = show.HallName;
					line.Quantity = (int)(request.Model?.Line.Count());
                    line.ShowEndAt = show.EndTime;
					line.ShowStartAt = show.StartTime;
					line.ShowStartEndTime = $"{show.StartTime} - {show.EndTime}";
					line.MovieTitle = show.MovieTitle;

					order.Amount += line.Price;
                    
                    await _orderMovieRepository.CreateAsync(line);
                }

				foreach (var item in request.Model?.Combos)
				{
					ProductDto product = null;
					ComboDto combo = null;
                    try
					{
                        product = await _catalogModuleApi.GetProductByIdAsync(item.ComboId);
                        combo = await _catalogModuleApi.GetComboByIdAsync(item.ComboId);
                    }
					catch (Exception)
					{
                        if (combo == null)
                        {
                            if (product == null)
                                return ResponseExceptionHelper.ErrorResponse<ComboDto>(ErrorCode.NotFound);
                        }
                    }

                    var line = new OrderCombo();
					line.OrderId = order.Id;
					line.ComboId = combo != null ? combo.Id : product.Id;
					line.ComboName = combo != null ? combo.Name : product.Name;
					line.ComboTypeName = string.Empty;
                    line.Price = combo != null ? combo.Price : product.Price;
                    line.Quantity = item.Quantity;

					order.Amount += line.Price * line.Quantity;
                    await _orderProductRepository.CreateAsync(line);
                }

				order.NetAmount = order.Amount;
                var isOrderSusccessed = await _orderRepository.CreateAsync(order);
                if (isOrderSusccessed)
				{
					await _unitOfWork.SaveChangesAsync();
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
