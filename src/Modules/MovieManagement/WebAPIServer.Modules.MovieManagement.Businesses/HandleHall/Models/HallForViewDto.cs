using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models.Base;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models
{
    public class HallForViewDto: HallBaseDto
	{
        public Guid Id { get; set; }
        public string CinemaName { get; set; } = default!;
        public byte TotalSeat { get; set; }
    }
    public class HallForViewDetailsDto: HallForViewDto
    {
        public ICollection<SeatForViewDto> Seats { get; set; } = new List<SeatForViewDto>();
    }
    public class SeatForViewDto
    {
        public Guid Id { get; set; }
        public string? SeatPosition { get; set; } = default!;
        public Guid SeatTypeId {  get; set; }
        public string SeatTypeName { get; set; } = default!;
        public double SeatTypePrice { get; set; } = default!;
	}
}