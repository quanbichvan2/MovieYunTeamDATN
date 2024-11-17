using Microsoft.EntityFrameworkCore;
using WebAPIServer.Modules.Booking.Businesses.Contracts;
using WebAPIServer.Modules.Booking.DataAccesses.Data;
using WebAPIServer.Modules.Booking.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Booking.DataAccesses.Repositories
{
	public class OrderRepository : BaseRepository<Order, BookingDbContext>, IOrderRepository
	{
		public OrderRepository(BookingDbContext context) : base(context) { }

		public override Task<Order?> GetByIdAsync(Guid? id)
		{
			return _context.Orders
				//.Include(x => x.Combos)
				//.Include(x => x.Products)
				//.Include(x => x.ShowSeats)
				.FirstOrDefaultAsync(x => x.Id.Equals(id));
		}
		public async Task<string> GetStatusByIdAsync(Guid statusId)
		{
			return await _context.OrderStatuses
				.Where(x => x.Id == statusId)
				.Select(x => x.Name).FirstAsync();
		}
	}
}