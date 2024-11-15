using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPIServer.Modules.Catalog.Domain.Entities;

namespace WebAPIServer.Modules.Catalog.DataAccesses.Data.Seeders
{
    public class ComboSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CatalogDbContext(serviceProvider.GetRequiredService<DbContextOptions<CatalogDbContext>>()))
            {
                if (!context.Combos.Any())
                {
                    context.Combos.AddRange(
                        new Combo
                        {
                            Id = Guid.NewGuid(),
                            Name = "Combo Thương 1",
                            Description = "Description1",
                            Image = "Image1",
                            Price = 100,
                            Code = "CB01",
                            CreatedAt = DateTime.UtcNow,
                            ModifiedAt = DateTime.UtcNow
                        },
                        new Combo
                        {
                            Id = Guid.NewGuid(),
                            Name = "Combo Cuốn 2",
                            Description = "Description2",
                            Image = "Image2",
                            Price = 200,
                            Code = "CB02",
                            CreatedAt = DateTime.UtcNow,
                            ModifiedAt = DateTime.UtcNow
                        },
                        new Combo
                        {
                            Id = Guid.NewGuid(),
                            Name = "Combo Đôi 3",
                            Description = "Description3",
                            Image = "Image3",
                            Price = 300,
                            Code = "CB03",
                            CreatedAt = DateTime.UtcNow,
                            ModifiedAt = DateTime.UtcNow
                        }
                    );
                    context.SaveChanges();
                }

            }
        }
    }
}
