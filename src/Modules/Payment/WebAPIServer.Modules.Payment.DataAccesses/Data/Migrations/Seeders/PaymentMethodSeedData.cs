using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPIServer.Modules.Payment.Domain.Entities;
using WebAPIServer.Modules.Tickets.DataAccesses.Data;
using WebAPIServer.Modules.Tickets.Domain.Entities;

namespace WebAPIServer.Modules.Payment.DataAccesses.Data.Migrations.Seeders
{
    public class PaymentMethodSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PaymentDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<PaymentDbContext>>()))
            {
                if (!context.PaymentMethods.Any())
                {
                    context.PaymentMethods.AddRange(
                    new PaymentMethod
                    {
                        Id = PaymentMethodContans.Cash,
                        Name = "Thanh toán tiền mặt",
                    },
                    new PaymentMethod
                    {
                        Id = PaymentMethodContans.CreditCard,
                        Name = "Thanh Toán Qua VnPay",
                    });
                    context.SaveChanges();
                }
            }

        }
    }
}
