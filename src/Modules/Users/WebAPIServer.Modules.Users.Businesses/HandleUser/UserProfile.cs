using AutoMapper;
using WebAPIServer.Modules.Users.Businesses.HandleUser.Models;
using WebAPIServer.Modules.Users.Domain.Entities;

namespace WebAPIServer.Modules.Users.Businesses.HandleUser
{
	public class UserProfile : Profile
    {
        public UserProfile()
        {
            Init();
        }
        private void Init()
        {
            CreateMap<User, UserForViewDto>();
            CreateMap<UserForCreateDto, User>();
            CreateMap<UserForUpdateDto, User>();
        }
    }
}
