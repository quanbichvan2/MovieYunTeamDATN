
using Microsoft.EntityFrameworkCore;
using WebAPIServer.Modules.Users.Domain.Entities;

namespace WebAPIServer.Modules.Users.DataAccesses.Data
{
	public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("users");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
