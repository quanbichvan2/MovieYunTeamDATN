using MediatR;
using OneOf;
using WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Commands
{
	public record UpdateTicketTypeCommand(Guid Id, TicketTypeForUpdateDto Model) : IRequest<OneOf<bool, ResponseException>>;
}