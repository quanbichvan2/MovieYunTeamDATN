using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPIServer.Modules.Identity.Domain.Entities;

namespace WebAPIServer.Modules.Identity.DataAccesses.Data.Seeders
{
	public class UserRoleIdentitySeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new IdentityDbContext(
				serviceProvider.GetRequiredService<DbContextOptions<IdentityDbContext>>()))
			{
				if (!context.UserRoles.Any())
				{
					context.UserRoles.AddRange(
						new UserRoleIdentity
						{
							Id = Guid.NewGuid(),
							RoleId = RoleIdentityConstants.Administrator,
							UserId = Guid.Parse("0c0d7375-1ccf-4985-818e-7d38f8fb8b03")
						},
						new UserRoleIdentity
						{
							Id = Guid.NewGuid(),
							RoleId = RoleIdentityConstants.Client,
							UserId = Guid.Parse("64dca04f-a3a5-4962-8404-5dda48979eeb")
						});
					context.SaveChanges();
				}
			}
		}
	}
}