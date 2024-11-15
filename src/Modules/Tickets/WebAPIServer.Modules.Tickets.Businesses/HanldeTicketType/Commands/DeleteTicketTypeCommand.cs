using MediatR;
using OneOf;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Commands
{
	public record DeleteTicketTypeCommand(Guid Id) : IRequest<OneOf<bool, ResponseException>>;
}
