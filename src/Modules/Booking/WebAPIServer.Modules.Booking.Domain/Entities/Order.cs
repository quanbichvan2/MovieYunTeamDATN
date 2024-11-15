using System.ComponentModel.DataAnnotations.Schema;
using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.Booking.Domain.Entities
{
	public class Order : BaseAuditableEntity
	{
		public double? Amount { get; set; }
		public double? SubAmount { get; set; }

		// Reference to users.Users
		public Guid? UserId { get; set; }
		public string? UserName { get; set; } = default!;

		// Reference to movie_management.Shows
		public Guid ShowId { get; set; }
		public string HallName { get; set; } = default!;
		public string CinemaName { get; set; } = default!;
		public DateTimeOffset? ShowStartAt { get; set; }
		public DateTimeOffset? ShowEndAt { get; set; }
		public string? ShowStartEndTime { get; set; } = default!;
		public string? MovieTitle { get; set; } = default!;

		// Reference to tickets.TicketTypes
		public Guid TicketTypeId { get; set; }
		public string TicketTypeName { get; set; } = default!;
		public double TicketTypePrice { get; set; }

		// Reference to voucher.Vouchers
		public Guid? VoucherId { get; set; }

		// Reference to Payment.
		public Guid? PaymentId { get; set; }

		[ForeignKey("OrderStatus")]
		public Guid OrderStatusId { get; set; }
		public string OrderStatus { get; set; } = default!;

		public virtual ICollection<OrderCombo>? Combos { get; set; } = new List<OrderCombo>();
		public virtual ICollection<OrderProduct>? Products { get; set; } = new List<OrderProduct>();
		public virtual ICollection<OrderShowSeat>? ShowSeats { get; set; } = new List<OrderShowSeat>();
		//public virtual ICollection<OrderTicketType>? TicketTypes { get; set; } = new List<OrderTicketType>();
	}
}