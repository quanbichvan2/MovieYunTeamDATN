using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.Booking.Domain.Entities
{
	public class OrderStatus : BaseEntity
	{
		public string Name { get; set; } = default!;

		public ICollection<Order>? Orders;
	}
	public static class OrderStatusConstants
	{
		public static Guid Requested = Guid.Parse("022857ee-4afd-4dcd-96f5-4815f0bd19be");
		public static Guid Pending = Guid.Parse("a26c5375-a522-4f59-a4fd-00d67e0c5e7a");
		public static Guid Confirmed = Guid.Parse("e1d556b3-5a7f-4b6d-87e9-76c0c75d3dbc");
		public static Guid CheckedIn = Guid.Parse("39cf2038-c11f-48d2-9d4e-ae9265e80091");
		public static Guid Canceled = Guid.Parse("f958b9af-8bbd-44db-b79e-3b0f7b8187b1");
		public static Guid Abandoned = Guid.Parse("d4f5901a-87eb-415c-9e93-d498be98ae51");
	}
}