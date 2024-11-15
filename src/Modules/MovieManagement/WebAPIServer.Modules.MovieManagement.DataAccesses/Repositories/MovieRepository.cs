using Microsoft.EntityFrameworkCore;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.DataAccesses.Data;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.MovieManagement.DataAccesses.Repositories
{
	public class MovieRepository : BaseRepository<Movie, MovieManagementDbContext>, IMovieRepository
	{
		public MovieRepository(MovieManagementDbContext context) : base(context) { }

		public override IQueryable<Movie> GetAll()
		{
			return _context.Movies
				.Include(x => x.Shows)
				.Include(x => x.Genres);
		}
		public override async Task<Movie?> FindByIdAsync(Guid? id)
		{
			var movie = await _context.Movies
				.Include(x => x.Shows)
				.Include(x => x.Genres)
				.Include(x => x.CastMembers)
				.FirstOrDefaultAsync(x => x.Id == id);
			return movie;
		}
	}
}
