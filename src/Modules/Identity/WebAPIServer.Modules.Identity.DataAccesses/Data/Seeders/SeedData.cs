namespace WebAPIServer.Modules.Identity.DataAccesses.Data.Seeders
{
	public class SeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			RoleIdentitySeedData.Initialize(serviceProvider);
			UserIdenttiySeedData.Initialize(serviceProvider);
			UserRoleIdentitySeedData.Initialize(serviceProvider);
		}
	}
}
