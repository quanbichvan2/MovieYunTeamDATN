using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models.Base;

namespace WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models
{
	public class OrderForViewDto : OrderBaseDto
	{
		//Order
		public Guid Id { get; set; }
		public string OrderStatus { get; set; } = default!;
		public double? Amount { get; set; }
		public double? SubAmount { get; set; }
		//User
		public string? UserName { get; set; }
		//Movie
		public string HallName { get; set; } = default!;
		public string CinemaName { get; set; } = default!;
		public DateTimeOffset? ShowStartAt { get; set; }
		public DateTimeOffset? ShowEndAt { get; set; }
		public string? ShowStartEndTime { get; set; } = default!;
		public string? MovieTitle { get; set; } = default!;
		//TicketType
		public string TicketTypeName { get; set; } = default!;
	}
	public class OrderForViewDetailsDto : OrderForViewDto
	{
		public IList<OrderComboForViewDto> Combos { get; set; } = new List<OrderComboForViewDto>();
		public IList<OrderProductForViewDto> Products { get; set; } = new List<OrderProductForViewDto>();
		public IList<OrderShowSeatForViewDto> ShowSeats { get; set; } = new List<OrderShowSeatForViewDto>();
	}
	public class OrderComboForViewDto : OrderForViewDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = default!;
		public int Quantity { get; set; }
		public double UnitPrice { get; set; }
		public double TotalPrice { get; set; }
	}
	public class OrderProductForViewDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = default!;
		public int Quantity { get; set; }
		public double UnitPrice { get; set; }
		public double TotalPrice { get; set; }
	}
	public class OrderShowSeatForViewDto
	{
		public Guid Id { get; set; }
		public string Position { get; set; } = default!;
		public string SeatTypeName { get; set; } = default!;
		public double SeatTypePrice { get; set; }
		public bool IsReseved { get; set; }
	}
}