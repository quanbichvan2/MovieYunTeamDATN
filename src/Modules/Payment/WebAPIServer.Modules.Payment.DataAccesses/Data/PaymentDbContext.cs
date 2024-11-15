using Microsoft.EntityFrameworkCore;
using WebAPIServer.Modules.Payment.Domain.Entities;
using WebAPIServer.Modules.Payment.Domain.Entities.Payment;

namespace WebAPIServer.Modules.Payment.DataAccesses.Data
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> context) : base(context) { }
        //public virtual DbSet<Payment> Payments { get; set; }
        //public virtual DbSet<PaymentTransaction> PaymentTracsactions { get; set; }

        //db rieng de luu cai vnpay test lan dau truoc khi update toan bo phan payment
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("payment");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            /*modelBuilder.Entity<Payment>()
            .HasMany(p => p.Transactions)
            .WithOne(t => t.Payment)
            .HasForeignKey(t => t.PaymentId);*/
        }
    }
}
