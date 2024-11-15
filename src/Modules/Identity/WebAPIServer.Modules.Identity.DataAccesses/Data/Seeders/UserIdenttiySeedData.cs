using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPIServer.Modules.Identity.Domain.Entities;

namespace WebAPIServer.Modules.Identity.DataAccesses.Data.Seeders
{
	public class UserIdenttiySeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new IdentityDbContext(
				serviceProvider.GetRequiredService<DbContextOptions<IdentityDbContext>>()))
			{
				if (!context.Users.Any())
				{
					context.Users.AddRange(
						new UserIdentity
						{
							Id = Guid.Parse("64dca04f-a3a5-4962-8404-5dda48979eeb"),
							Email = "client@gmail.com",
							PasswordHash = "$2a$11$vgBIAKF5LWviipdwNtd5k.EP50dkhYU/d3Du6u8OsPsZCztjJnse2"
						},
						new UserIdentity
						{
							Id = Guid.Parse("0c0d7375-1ccf-4985-818e-7d38f8fb8b03"),
							Email = "admin@gmail.com",
                            //PasswordHash = "$2a$11$vgBIAKF5LWviipdwNtd5k.EP50dkhYU/d3Du6u8OsPsZCztjJnse2"
                            PasswordHash = "335f275395e11eecfd5f0a5629cdd4b1"

                        }
                        );
					context.SaveChanges();
				}
			}
		}
	}
}