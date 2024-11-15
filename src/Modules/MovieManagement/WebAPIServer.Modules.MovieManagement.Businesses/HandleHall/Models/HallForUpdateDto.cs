using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models.Base;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models
{
    public class HallForUpdateDto: HallBaseDto
    {
        public ICollection<SeatForUpdateDto> Seats { get; set; } = new List<SeatForUpdateDto>();
    }
    public class SeatForUpdateDto
    {
        public Guid Id { get; set; }
        public Guid SeatTypeId { get; set; }
    }
}