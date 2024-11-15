using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.Booking.Domain.Entities
{
	public class OrderShowSeat : BaseEntity
	{
		public bool IsReseved {  get; set; }
		 
		[ForeignKey("Order")]
		public Guid OrderId { get; set; }
		public Order Order { get; set; } = default!;

		// Reference to movie_management.Seat
		public Guid SeatId { get; set; }
        public string SeatPosition { get; set; } = default!;
		public string SeatTypeName { get; set; } = default!;
		public double SeatTypePrice { get; set; }

		// Reference to movie_management.Show
		public Guid? ShowId { get; set; }
	}
}
