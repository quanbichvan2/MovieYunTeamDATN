using System.ComponentModel.DataAnnotations.Schema;
using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.Booking.Domain.Entities
{
	public class OrderProduct1 : BaseEntity
	{
		public int Quantity { get; set; }
		public double UnitPrice { get; set; }
		public double TotalPrice { get; set; }

		[ForeignKey("Order")]
		public Guid OrderId { get; set; }
		public Order Order { get; set; } = default!;

		// Reference to catalog.Combo
		public Guid ProductId { get; set; }
		public string ProductName { get; set; } = default!;
	}
}
