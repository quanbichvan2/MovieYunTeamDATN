using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models.Base;
using WebAPIServer.Modules.MovieManagement.Domain.Enums;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models
{
	public class ShowForViewDto : ShowBaseDto
	{
		public Guid Id { get; set; }
		public string HallName { get; set; } = default!;
		//public string CinemaName { get; set; } = default!;
		public string MovieTitle { get; set; }
		public List<GenreMovieDto> Genres { get; set; } = new List<GenreMovieDto>();
		public List<ListTime> ListTime { get; set; } = new List<ListTime>();
        public byte MovieRuntimeMinutes { get; set; }
		public string MoviePosterImage { get; set; } = default!;
        public AgeRating AgeRating { get; set; }
		
    }

	public class ListTime
    {
        public DateTimeOffset StartTime { get; set; }
        public List<ShowTimeDto> ShowTimes { get; set; } = new List<ShowTimeDto>();
    }

	public class GenreMovieDto
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
	public class ShowTimeDto
	{
        public string Time { get; set; }
	}
	public class ShowSeatForViewDto
	{
		public Guid Id { get; set; }
		public Guid SeatId { get; set; }
		public string SeatType { get; set; } = default!;
		public string SeatPosition { get; set; } = default!;
		public bool IsReserved { get; set; }
	}
}
