using WebAPIServer.Modules.Booking.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Booking.Businesses.Contracts
{
	public interface IOrderRepository : IRepository<Order>
	{
		Task<string> GetStatusByIdAsync(Guid statusId);
	}
}
