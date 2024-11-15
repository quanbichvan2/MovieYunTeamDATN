using AutoMapper;
using WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Models;
using WebAPIServer.Modules.Catalog.Domain.Entities;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleProduct
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            Init();
        }
        private void Init()
        {
            CreateMap<Product, ProductForViewDto>();
            CreateMap<ProductForCreateDto, Product>();
            CreateMap<ProductForUpdateDto, Product>();
        }
    }
}
