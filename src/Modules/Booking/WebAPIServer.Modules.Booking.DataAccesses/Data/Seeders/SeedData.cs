namespace WebAPIServer.Modules.Booking.DataAccesses.Data.Seeders
{
	public class SeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			OrderStatusSeedData.Initialize(serviceProvider);
		}
	}
}
