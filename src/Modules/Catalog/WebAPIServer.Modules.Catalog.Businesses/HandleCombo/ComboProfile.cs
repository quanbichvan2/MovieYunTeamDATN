using AutoMapper;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models;
using WebAPIServer.Modules.Catalog.Domain.Entities;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCombo
{
    public class ComboProfile : Profile
    {
        public ComboProfile()
        {
            Init();
        }
        public void Init()
        {
            CreateMap<Combo, ComboForViewDto>();
            CreateMap<Combo, ComboForViewDetailsDto>()
                 .ForMember(c => c.Products, p => p.MapFrom(s => s.Products));
            CreateMap<ComboProduct, ComboProductForViewDto>()
                .ForMember(c => c.Id, p => p.MapFrom(s => s.ProductId))
                .ForMember(c => c.Name, p => p.MapFrom(s => s.ProductName))
                .ForMember(c => c.Image, p => p.MapFrom(s => s.ProductImage));

            CreateMap<ComboForCreateDto, Combo>()
                .ForMember(src => src.Products, opt => opt.MapFrom(src => src.Products));
            CreateMap<ComboProductForCreateDto, ComboProduct>()
                .ForMember(src => src.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(src => src.Id, opt => opt.Ignore())
                .ForMember(src => src.ComboId, opt => opt.Ignore())
                .ForMember(src => src.Combo, opt => opt.Ignore());

            CreateMap<ComboForUpdateDto, Combo>()
                .ForMember(src => src.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(src => src.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(src => src.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(src => src.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(src => src.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(src => src.Products, opt => opt.MapFrom(src => src.Products))
                .ForMember(src => src.Id, opt => opt.Ignore());
                //.ForMember(dest => dest.Products, opt => opt.Ignore());
            CreateMap<ComboProductForUpdateDto, ComboProduct>()
                .ForMember(src => src.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(src => src.ComboId, opt => opt.Ignore())
                .ForMember(src => src.Id, opt => opt.Ignore())
                .ForMember(src => src.Combo, opt => opt.Ignore());
        }
    }
}
