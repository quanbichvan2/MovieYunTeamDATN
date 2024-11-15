using WebAPIServer.Modules.Tickets.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Tickets.Businesses.Contracts.Repositories
{
    public interface ITicketTypeRepository : IRepository<TicketType>
    {
    }
}
