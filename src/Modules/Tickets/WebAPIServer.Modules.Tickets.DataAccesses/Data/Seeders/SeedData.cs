namespace WebAPIServer.Modules.Tickets.DataAccesses.Data.Seeders
{
	public class SeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			TicketTypeSeedData.Initialize(serviceProvider);
		}
	}
}
