using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPIServer.Modules.Catalog.Domain.Entities;

namespace WebAPIServer.Modules.Catalog.DataAccesses.Data.Seeders
{
    public class ProductSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CatalogDbContext(serviceProvider.GetRequiredService<DbContextOptions<CatalogDbContext>>()))
            {
                if (!context.Products.Any())
                {
                    context.Products.AddRange(
                    new Product
                    {
                        Id = Guid.Parse("9ff8dc71-cee7-4cd8-b4ae-91e723331ffa"),
                        Name = "Bắp Thường",
                        Image = "https://propercorn.com.vn/wp-content/uploads/2020/11/B%E1%BA%AFp-rang-b%C6%A1-Propercorn-Proper-cinema-scaled.jpg",
                        Description = "",
                        Price = 35000,
                        CategoryId = CategoryConstants.Foods,
                        Code = "bap",
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.Parse("65fcf80b-8745-4e71-b39a-51daca478a6b"),
                        Name = "7Up",
                        Image = "https://storage.googleapis.com/sc_pcm_product/prod/2024/3/27/46159-8934588022111.jpg",
                        Description = "",
                        Price = 30000,
                        CategoryId = CategoryConstants.Drinks,
                        Code = "7up",
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.Parse("c78eaf73-8217-4c69-88be-836f6d70ddb6"),
                        Name = "Coca Cola",
                        Image = "https://www.coca-cola.com/content/dam/onexp/vn/home-image/coca-cola/Coca-Cola_OT%20320ml_VN-EX_Desktop.png",
                        Description = "",
                        Price = 30000,
                        CategoryId = CategoryConstants.Drinks,
                        Code = "coca",
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.Parse("c03d8a9a-1a73-4f43-bab6-62614e1f18ff"),
                        Name = "Bắp Carameo",
                        Image = "https://propercorn.com.vn/wp-content/uploads/2020/11/B%E1%BA%AFp-rang-b%C6%A1-Propercorn-Proper-cinema-scaled.jpg",
                        Description = "",
                        Price = 40000,
                        CategoryId = CategoryConstants.Foods,
                        Code = "bap Carameo",
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.Parse("d4345b75-8391-435b-ade1-b5e8b9324cab"),
                        Name = "Bắp Phô Mai",
                        Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSTr4d46C5zp0As9z77CPgV9qTd3PUvX5QaOQ&s",
                        Description = "",
                        Price = 40000,
                        CategoryId = CategoryConstants.Foods,
                        Code = "bap pho mai",
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = DateTime.UtcNow
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
