using MediatR;
using WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Models;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Queries
{
	public record GetAllTicketTypesQuery(Filter Filter) : IRequest<IEnumerable<TicketTypeForViewDto>>;
}
