using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.DataAccesses.Data;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.MovieManagement.DataAccesses.Repositories
{
	public class ShowRepository : BaseRepository<Show, MovieManagementDbContext>, IShowRepository
	{
		public ShowRepository(MovieManagementDbContext context) : base(context) { }
	}
}
