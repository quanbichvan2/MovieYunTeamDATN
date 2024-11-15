using Microsoft.EntityFrameworkCore;
using WebAPIServer.Modules.Catalog.Domain.Entities;

namespace WebAPIServer.Modules.Catalog.DataAccesses.Data
{
	public class CatalogDbContext : DbContext
	{
		public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<Combo> Combos { get; set; }
		public virtual DbSet<ComboProduct> ComboProducts { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("catalog");
			modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
		}
	}
}