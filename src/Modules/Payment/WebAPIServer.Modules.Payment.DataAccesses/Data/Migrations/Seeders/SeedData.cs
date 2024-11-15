using WebAPIServer.Modules.Tickets.DataAccesses.Data.Seeders;

namespace WebAPIServer.Modules.Payment.DataAccesses.Data.Migrations.Seeders
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            PaymentMethodSeedData.Initialize(serviceProvider);
        }
    }
}
