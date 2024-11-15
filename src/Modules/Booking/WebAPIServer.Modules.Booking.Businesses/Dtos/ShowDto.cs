namespace WebAPIServer.Modules.Booking.Businesses.Dtos
{
	public class ShowDto
	{
		public Guid Id { get; set; }
		public string MovieTitle { get; set; } = default!;
		public byte MovieRuntimeMinutes { get; set; }
		public DateTimeOffset StartTime { get; set; }
		public DateTimeOffset EndTime { get; set; }
		public Guid CinemaHallId { get; set; } = default!;
		public string HallName { get; set; } = default!;
		public string CinemaName { get; set; } = default!;
	}
} 
