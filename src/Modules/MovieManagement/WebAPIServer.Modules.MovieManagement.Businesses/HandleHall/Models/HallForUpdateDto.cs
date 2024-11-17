using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models.Base;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models
{
    public class HallForUpdateDto: HallBaseDto
    {
        //public ICollection<SeatForUpdateDto> Seats { get; set; } = new List<SeatForUpdateDto>();

        public byte SeatColumn { get; set; }
        public byte SeatRow { get; set; }
        public Guid TypeId { get; set; }
    }
    public class SeatForUpdateDto
    {
        public Guid Id { get; set; }
        public Guid SeatTypeId { get; set; }
    }
}