using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models.Base;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models
{
	public class MovieForViewDto : MovieBaseDto
	{
		public Guid Id { get; set; }
		public string? DirectorName { get; set; }
		public IEnumerable<MovieShowForViewDto> Shows { get; set; } = new List<MovieShowForViewDto>();
		public IEnumerable<MovieGenreForViewDto> Genres { get; set; } = new List<MovieGenreForViewDto>();
	}
	public class MovieForViewDetailDto : MovieForViewDto
	{
		public IEnumerable<MovieCastMemberForViewDto> CastMembers { get; set; } = new List<MovieCastMemberForViewDto>();
	}
	public class MovieShowForViewDto()
	{
		public Guid Id { get; set; }
		public string HallName { get; set; } = default!;
		public DateTimeOffset StartTime { get; set; }
	}
	public class MovieGenreForViewDto()
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = default!;
	}
	public class MovieCastMemberForViewDto()
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = default!;
	}
}