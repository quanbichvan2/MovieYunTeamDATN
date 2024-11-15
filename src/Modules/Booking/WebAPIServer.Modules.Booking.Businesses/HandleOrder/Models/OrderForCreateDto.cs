using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models.Base;

namespace WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models
{
	public class OrderForCreateDto : OrderBaseDto
	{
		public IList<OrderComboForCreateDto>? Combos { get; set; } = new List<OrderComboForCreateDto>();
		public IList<OrderProductForCreateDto>? Products { get; set; } = new List<OrderProductForCreateDto>();
		public IList<OrderShowSeatForCreateDto>? ShowSeats { get; set; } = new List<OrderShowSeatForCreateDto>();
		//public IList<OrderTicketTypeForCreateDto>? TicketTypes { get; set; } = new List<OrderTicketTypeForCreateDto>();
	}
	public class OrderProductForCreateDto
	{
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }
	}
	public class OrderComboForCreateDto
	{
		public Guid ComboId { get; set; }
		public int Quantity { get; set; }
	}
	public class OrderShowSeatForCreateDto
	{
		public Guid SeatId { get; set; }
	}
	/*public class OrderTicketTypeForCreateDto
	{
		public Guid TicketTypeId { get; set; }
		public int Quantity { get; set; }
	}*/
}
