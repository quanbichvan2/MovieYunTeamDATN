namespace WebAPIServer.Modules.Vouchers.DataAccesses.Data.Seeder
{
	public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            VoucherSeedData.Initialize(serviceProvider);
        }
    }
}
