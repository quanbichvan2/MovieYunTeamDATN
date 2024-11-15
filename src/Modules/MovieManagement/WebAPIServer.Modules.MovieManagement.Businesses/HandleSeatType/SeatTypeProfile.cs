using AutoMapper;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType
{
    public class SeatTypeProfile: Profile
    {
        public SeatTypeProfile()
        {
            Init();
        }

        private void Init()
        {
            CreateMap<SeatType, SeatTypeForViewDto>();
        }
    }
}
