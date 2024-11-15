using MediatR;
using OneOf;
using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Booking.Businesses.HandleOrder.Queries
{
	public record GetOrdersAllQuery(Filter Filter) : IRequest<PaginatedList<OrderForViewDto>>;
}
