using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models.Base;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models
{
    public class HallForCreateDto: HallBaseDto
    {
        public byte SeatColumn {  get; set; }
        public byte SeatRow { get; set; }

    }
}
