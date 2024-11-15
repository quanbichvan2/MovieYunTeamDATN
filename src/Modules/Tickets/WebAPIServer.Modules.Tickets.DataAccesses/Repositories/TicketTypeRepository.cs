using Microsoft.EntityFrameworkCore;
using WebAPIServer.Modules.Tickets.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Tickets.DataAccesses.Data;
using WebAPIServer.Modules.Tickets.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Tickets.DataAccesses.Repositories
{
	public class TicketTypeRepository : BaseRepository<TicketType, TicketsDbContext>, ITicketTypeRepository
    {
        public TicketTypeRepository(TicketsDbContext context) : base(context)
        {
            
        }
    }
}
