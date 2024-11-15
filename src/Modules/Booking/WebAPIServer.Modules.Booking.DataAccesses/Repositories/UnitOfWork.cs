using WebAPIServer.Modules.Booking.Businesses.Contracts;
using WebAPIServer.Modules.Booking.DataAccesses.Data;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Booking.DataAccesses.Repositories
{
	public class UnitOfWork : BaseUnitOfWork<BookingDbContext>, IUnitOfWork
	{
		public UnitOfWork(BookingDbContext context) : base(context) { }
	}
}
