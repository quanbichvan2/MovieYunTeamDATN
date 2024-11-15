using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.Tickets.Domain.Entities
{
	public class TicketType : BaseAuditableEntity
	{
		public string Code { get; set; } = default!;
		public string Name { get; set; } = default!;
		public double Price { get; set; } = default;
	}
	public static class TicketTypeConstants
	{
		/// <summary>
		/// vé ngày thường hoặc vé giờ thường
		/// </summary>
		public static Guid Regular = Guid.Parse("ce55c742-c9b9-4750-b37e-7d3152ff6f93");
		/// <summary>
		/// vé giờ cao điểm
		/// </summary>
		public static Guid RushHour = Guid.Parse("ce55c742-c9b9-4750-b37e-7d3152ff6f95");
		/// <summary>
		/// vé cho ngày lễ
		/// </summary>
		public static Guid Vacation = Guid.Parse("ce55c742-c9b9-4750-b37e-7d3152ff6f90");
	}
}
