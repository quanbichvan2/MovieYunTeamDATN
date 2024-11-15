using System.ComponentModel.DataAnnotations.Schema;
using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.Booking.Domain.Entities
{
	public class OrderCombo : BaseEntity
	{
		public int Quantity { get; set; }
		public double UnitPrice { get; set; }
		public double TotalPrice { get; set; }

		[ForeignKey("Order")]
		public Guid OrderId { get; set; }
		public Order Order { get; set; } = default!;

		// Reference to catalog.Combo
		public Guid ComboId { get; set; }
		public string ComboName { get; set; } = default!;
	}
}
