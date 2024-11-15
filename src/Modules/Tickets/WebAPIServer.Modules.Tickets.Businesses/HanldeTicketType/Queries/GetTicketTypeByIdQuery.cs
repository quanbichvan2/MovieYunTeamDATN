using MediatR;
using OneOf;
using WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Queries
{
	public record GetTicketTypeByIdQuery(Guid Id) : IRequest<OneOf<TicketTypeForViewDto, ResponseException>>;
}
