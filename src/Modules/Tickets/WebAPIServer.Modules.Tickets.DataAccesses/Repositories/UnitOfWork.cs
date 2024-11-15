using WebAPIServer.Modules.Tickets.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Tickets.DataAccesses.Data;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Tickets.DataAccesses.Repositories
{
	public class UnitOfWork : BaseUnitOfWork<TicketsDbContext>, IUnitOfWork
	{
		public UnitOfWork(TicketsDbContext context) : base(context) { }
	}
}