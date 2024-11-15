using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPIServer.Modules.Booking.Businesses.Contracts;
using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models;
using WebAPIServer.Modules.Booking.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Extensions;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Booking.Businesses.HandleOrder.Queries
{
	public class GetOrdersAllQueryHandler : IRequestHandler<GetOrdersAllQuery, PaginatedList<OrderForViewDto>>
	{
		private readonly IMapper _mapper;
		private readonly IOrderRepository _orderRepository;
		private readonly ILogger<GetOrdersAllQueryHandler> _logger;
		public GetOrdersAllQueryHandler(IMapper mapper,
			IOrderRepository orderRepository,
			ILogger<GetOrdersAllQueryHandler> logger)
		{
			_logger = logger;
			_orderRepository = orderRepository;
			_mapper = mapper;
		}
		public async Task<PaginatedList<OrderForViewDto>> Handle(GetOrdersAllQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var query = _orderRepository.GetAll();
				var allowedOrderProperties = new List<string> 
				{ 
					"UserName" 
				};
				if (!string.IsNullOrEmpty(request.Filter.SearchTerm))
				{
					string search = request.Filter.SearchTerm.ToLower().Trim();
					query = query.Where(x => EF.Functions.Unaccent(x.UserName!).ToLower().Contains(search));
				}
				query = query.SortBy(request.Filter?.SortColumn, allowedOrderProperties, request.Filter.IsDescending);
				var paginatedOrders = await PaginatedList<Order>.CreateAsync(
					query,
					request.Filter.PageIndex,
					request.Filter.PageSize,
					cancellationToken);

				var castMemberViewDtos = _mapper.Map<List<OrderForViewDto>>(paginatedOrders.Items);

				var paginatedOrderViews = new PaginatedList<OrderForViewDto>(
					castMemberViewDtos,
					request.Filter.PageIndex,
					request.Filter.PageSize,
					paginatedOrders.TotalCount);
				return paginatedOrderViews;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error fetching Orders: {ex.Message}");
				throw new NullReferenceException(nameof(Handle), ex);
			}
		}
	}
}
