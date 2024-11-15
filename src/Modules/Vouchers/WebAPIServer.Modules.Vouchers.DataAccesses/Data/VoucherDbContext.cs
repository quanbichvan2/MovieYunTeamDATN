using Microsoft.EntityFrameworkCore;
using WebAPIServer.Modules.Vouchers.Domain.Entities;

namespace WebAPIServer.Modules.Vouchers.DataAccesses.Data
{
    public class VoucherDbContext : DbContext
    {
        public VoucherDbContext(DbContextOptions<VoucherDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Voucher> Vouchers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("voucher");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
