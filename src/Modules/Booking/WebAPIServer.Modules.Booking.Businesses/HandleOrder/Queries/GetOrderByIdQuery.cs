using MediatR;
using OneOf;
using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Booking.Businesses.HandleOrder.Queries
{
	public record GetOrderByIdQuery(Guid Id) : IRequest<OneOf<OrderForViewDetailsDto, ResponseException>>;
}