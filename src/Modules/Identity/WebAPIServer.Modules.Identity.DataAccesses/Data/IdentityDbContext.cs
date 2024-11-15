using Microsoft.EntityFrameworkCore;
using WebAPIServer.Modules.Identity.Domain.Entities;

namespace WebAPIServer.Modules.Identity.DataAccesses.Data
{
	public class IdentityDbContext : DbContext
	{
		public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

		public virtual DbSet<UserIdentity> Users { get; set; }
		public virtual DbSet<UserRoleIdentity> UserRoles { get; set; }
		public virtual DbSet<RoleIdentity> Roles { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("identity");
			modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
		}
	}
}