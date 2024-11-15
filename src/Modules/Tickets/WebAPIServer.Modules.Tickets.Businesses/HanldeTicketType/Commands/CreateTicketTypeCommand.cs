using MediatR;
using OneOf;
using WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Commands
{
	public record CreateTicketTypeCommand(TicketTypeForCreateDto Model) : IRequest<OneOf<Guid, ResponseException>>;
}
