using AutoMapper;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleShow
{
    public class ShowProfile : Profile
    {
        public ShowProfile()
        {
            CreateMap<ShowForCreateDto, Show>();
            CreateMap<ShowForUpdateDto, Show>();
            CreateMap<ShowForUpdateDto, Show>();
                //.ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToUniversalTime()));
                //.ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToUniversalTime()));
            CreateMap<Show, ShowForViewDto>();
            CreateMap<Show, ShowTimeDto>();
        }
    }
}