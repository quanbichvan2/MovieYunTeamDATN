using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models.Base;
using WebAPIServer.Modules.Booking.Domain.Entities;

namespace WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models
{
	public class OrderForCreateDto : OrderBaseDto
	{
        public Guid PaymentId { get; set; }
        public OrderStatus Status { get; set; }
        public IList<OrderComboForCreateDto>? Combos { get; set; } = new List<OrderComboForCreateDto>();
        public IList<OrderLineForCreateDto>? Line { get; set; } = new List<OrderLineForCreateDto>();
    }

    public class OrderLineForCreateDto
    {
        public Guid ShowId { get; set; }
        public Guid? VoucherId { get; set; }
        public Guid SeatId { get; set; }
        public Guid TypeId { get; set; }
    }

    public class OrderComboForCreateDto
	{
		public Guid ComboId { get; set; }
        public int Quantity { get; set; }
	}

	/*public class OrderTicketTypeForCreateDto
	{
		public Guid TicketTypeId { get; set; }
		public int Quantity { get; set; }
	}*/
}
