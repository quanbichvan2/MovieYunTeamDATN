using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models.Base;

namespace WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models
{
	public class OrderForUpdateDto: OrderBaseDto
	{
		public Guid OrderStatusId { get; set; }
	}
}
