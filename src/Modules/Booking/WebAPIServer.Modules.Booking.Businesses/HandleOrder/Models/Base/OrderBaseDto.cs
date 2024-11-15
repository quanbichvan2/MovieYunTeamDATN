namespace WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models.Base
{
	public class OrderBaseDto
	{
		public Guid? UserId { get; set; }
		public Guid? ShowId { get; set; }
		public Guid? VoucherId { get; set; }
		public Guid? TicketTypeId { get; set; }
	}
}
