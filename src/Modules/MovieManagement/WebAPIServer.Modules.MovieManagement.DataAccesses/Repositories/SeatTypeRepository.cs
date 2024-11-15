using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.DataAccesses.Data;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.MovieManagement.DataAccesses.Repositories
{
	public class SeatTypeRepository : BaseRepository<SeatType, MovieManagementDbContext>, ISeatTypeRepository
	{
		public SeatTypeRepository(MovieManagementDbContext context) : base(context) { }
	}
}