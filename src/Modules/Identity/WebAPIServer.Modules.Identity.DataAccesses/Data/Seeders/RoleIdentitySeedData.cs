using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPIServer.Modules.Identity.Domain.Entities;

namespace WebAPIServer.Modules.Identity.DataAccesses.Data.Seeders
{
	public static class RoleIdentitySeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new IdentityDbContext(
				serviceProvider.GetRequiredService<DbContextOptions<IdentityDbContext>>()))
			{
				if (!context.Roles.Any())
				{
					context.Roles.AddRange(
					new RoleIdentity
					{
						Id = RoleIdentityConstants.Client,
						Name = "client",
						NormalizeName = "CLIENT"
					},
					new RoleIdentity
					{
						Id = RoleIdentityConstants.Administrator,
						Name = "administrator",
						NormalizeName = "ADMINISTRATOR"
					});
					context.SaveChanges();
				}
			}
		}
	}
}
