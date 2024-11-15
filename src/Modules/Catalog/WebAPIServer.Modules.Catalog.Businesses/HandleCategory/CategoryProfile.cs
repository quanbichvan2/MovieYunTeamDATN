using AutoMapper;
using WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Models;
using WebAPIServer.Modules.Catalog.Domain.Entities;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCategory
{
	public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            Init();
        }
        private void Init()
        {
            CreateMap<Category, CategoryForViewDto>();
            CreateMap<CategoryForCreateDto, Category>();
            CreateMap<CategoryForUpdateDto, Category>();
        }
    }
}
