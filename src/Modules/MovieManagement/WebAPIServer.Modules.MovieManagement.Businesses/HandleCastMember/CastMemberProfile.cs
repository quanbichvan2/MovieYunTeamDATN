using AutoMapper;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember
{
    public class CastMemberProfile: Profile
    {
        public CastMemberProfile()
        {
            Init();  
        }

        private void Init()
        {
            CreateMap<CastMember, CastMemberForViewDto>();
            CreateMap<CastMemberForCreateDto, CastMember>();
            CreateMap<CastMemberForUpdateDto, CastMember>();
        }
    }
}
