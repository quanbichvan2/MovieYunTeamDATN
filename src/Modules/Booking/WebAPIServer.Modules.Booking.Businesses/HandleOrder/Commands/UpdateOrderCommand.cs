using MediatR;
using OneOf;
using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Booking.Businesses.HandleOrder.Commands
{
	public record UpdateOrderCommand(OrderForUpdateDto Model, Guid Id) : IRequest<OneOf<bool, ResponseException>>;
}
