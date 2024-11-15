namespace WebAPIServer.Modules.Booking.Businesses.Dtos
{
	public class HallDto
	{
		public IList<SeatDto> Seats { get; set; } = new List<SeatDto>();
	}
}