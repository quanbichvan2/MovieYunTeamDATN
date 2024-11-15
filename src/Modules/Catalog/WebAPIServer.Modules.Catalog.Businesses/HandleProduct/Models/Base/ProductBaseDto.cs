using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Models.Base
{
    public class ProductBaseDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
