using AutoMapper;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            Init();  
        }

        private void Init()
        {
            CreateMap<Genre, GenreForViewDto>();
            CreateMap<GenreForCreateDto, Genre>();
            CreateMap<GenreForUpdateDto, Genre>();
        }
    }
}
