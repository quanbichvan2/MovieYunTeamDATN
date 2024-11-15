using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Booking.Businesses.Contracts;
using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models;
using WebAPIServer.Modules.Booking.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Booking.Businesses.HandleOrder.Queries
{
	public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OneOf<OrderForViewDetailsDto, ResponseException>>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;
		private readonly ILogger<GetOrderByIdQueryHandler> _logger;
		public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<GetOrderByIdQueryHandler> logger)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
			_logger = logger;
		}
		public async Task<OneOf<OrderForViewDetailsDto, ResponseException>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var order = await _orderRepository.GetByIdAsync(request.Id);
				if (order == null)
				{
					return ResponseExceptionHelper.ErrorResponse<Order>(ErrorCode.NotFound);
				}
				return _mapper.Map<OrderForViewDetailsDto>(order);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return ResponseExceptionHelper.ErrorResponse<Order>(ErrorCode.OperationFailed);
			}
		}
	}
}
