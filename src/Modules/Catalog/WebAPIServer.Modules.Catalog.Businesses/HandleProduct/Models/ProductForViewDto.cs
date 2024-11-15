using WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Models.Base;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Models
{
    public class ProductForViewDto : ProductBaseDto
    {
        public Guid Id { get; set; }
    }
}