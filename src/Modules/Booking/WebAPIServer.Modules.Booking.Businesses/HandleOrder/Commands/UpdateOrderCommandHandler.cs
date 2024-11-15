using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Booking.Businesses.Contracts;
using WebAPIServer.Modules.Booking.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Booking.Businesses.HandleOrder.Commands
{
	public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OneOf<bool, ResponseException>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IOrderRepository _orderRepository;
		private readonly ILogger<UpdateOrderCommandHandler> _logger;
		public UpdateOrderCommandHandler(IMapper mapper,
			IUnitOfWork unitOfWork,
			IOrderRepository orderRepository,
			ILogger<UpdateOrderCommandHandler> logger)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_orderRepository = orderRepository;
			_logger = logger;
		}

		public async Task<OneOf<bool, ResponseException>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var orderDto = request.Model;
				var order = await _orderRepository.GetByIdAsync(request.Id);
				if (order == null)
				{
					return ResponseExceptionHelper.ErrorResponse<Order>(ErrorCode.NotFound);
				}

				if (order.OrderStatusId == OrderStatusConstants.Confirmed && orderDto.OrderStatusId == OrderStatusConstants.CheckedIn)
				{
					if (DateTime.Now >= order.ShowStartAt)
					{
						//Confirmed  -> CheckedIn
						order.OrderStatusId = OrderStatusConstants.CheckedIn;
						order.OrderStatus = await _orderRepository.GetStatusByIdAsync(OrderStatusConstants.CheckedIn);
					}
					else
					{
						return ResponseExceptionHelper.ErrorResponse<Order>(ErrorCode.UpdateError, "Order can only be checked in once the show has started.");
					}
				}
				_mapper.Map(orderDto, order);
				_orderRepository.Update(order);
				await _unitOfWork.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
