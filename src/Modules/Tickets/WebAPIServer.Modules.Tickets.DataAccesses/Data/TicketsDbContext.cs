using Microsoft.EntityFrameworkCore;
using WebAPIServer.Modules.Tickets.Domain.Entities;

namespace WebAPIServer.Modules.Tickets.DataAccesses.Data
{
	public class TicketsDbContext : DbContext
	{
		public TicketsDbContext(DbContextOptions<TicketsDbContext> context) : base(context) { }
		public virtual DbSet<TicketType> TicketTypes { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("tickets");
			modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
		}
	}
}
