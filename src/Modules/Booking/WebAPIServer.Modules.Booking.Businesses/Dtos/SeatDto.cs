namespace WebAPIServer.Modules.Booking.Businesses.Dtos
{
	public class SeatDto
	{
		public Guid Id { get; set; }
        public string SeatTypeName { get; set; } = default!;
		public string SeatPosition { get; set; } = default!;
        public double SeatTypePrice { get; set; }
	}
}