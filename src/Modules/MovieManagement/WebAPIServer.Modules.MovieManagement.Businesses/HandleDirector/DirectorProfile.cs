using AutoMapper;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector
{
    public class DirectorProfile: Profile
    {
        public DirectorProfile()
        {
            Init();  
        }

        private void Init()
        {
            CreateMap<Director, DirectorForViewDto>();
            CreateMap<DirectorForCreateDto, Director>();
            CreateMap<DirectorForUpdateDto, Director>();
        }
    }
}
