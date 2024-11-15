using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;

namespace WebAPIServer.Modules.MovieManagement.DataAccesses.Data.Seeders
{
	internal static class DirectorSeedData
	{
		public static void Initialize(this IServiceProvider serviceProvider)
		{
			using (var context = new MovieManagementDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieManagementDbContext>>()))
			{
				if (!context.Directors.Any())
				{
					context.Directors.AddRange(
						new Director { Id = Guid.Parse("95b1fd33-39cc-421d-abd0-93c180ecf291"), Name = "Anthony Russo và Joe Russo" },
						new Director { Id = Guid.Parse("9970977e-80b6-46c9-929b-8eb13fd8ed25"), Name = "Jake Kasdan" },
						new Director { Id = Guid.Parse("13701a86-a3f6-4973-a7c6-82e5570772bd"), Name = "Kelly Marcel" },
						new Director { Id = Guid.Parse("43719e6c-78f6-444a-af6a-a2632799f8df"), Name = "Coralie Fargeat" },
						new Director { Id = Guid.Parse("370946a7-a527-469e-91b7-15f41eeba7d7"), Name = "Panu Aree" },
						new Director { Id = Guid.Parse("495222e7-80fa-419f-9a61-44849031a143"), Name = "E.Oni" },
						new Director { Id = Guid.Parse("bbe9eec1-09d0-4a3b-88ff-26ddb8dab2ab"), Name = "Vũ Ngọc Đãng" }
					);
					context.SaveChanges();
				}
			}
		}
	}
}
