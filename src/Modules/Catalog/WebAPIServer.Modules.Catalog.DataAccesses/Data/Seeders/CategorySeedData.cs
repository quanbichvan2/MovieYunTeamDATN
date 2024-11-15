using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPIServer.Modules.Catalog.Domain.Entities;

namespace WebAPIServer.Modules.Catalog.DataAccesses.Data.Seeders
{
    public class CategorySeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CatalogDbContext(serviceProvider.GetRequiredService<DbContextOptions<CatalogDbContext>>()))
            {
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                    new Category
                    {
                        Id = CategoryConstants.Foods,
                        Name = "Bắp",
                        Code = "PopCorn"
                    },
                    new Category
                    {
                        Id = CategoryConstants.Drinks,
                        Name = "Nước",
                        Code = "Drinks"
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}