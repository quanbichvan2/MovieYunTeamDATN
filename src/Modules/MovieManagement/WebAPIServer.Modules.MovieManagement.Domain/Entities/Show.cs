using System.ComponentModel.DataAnnotations.Schema;
using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.MovieManagement.Domain.Entities
{
    public class Show : BaseAuditableEntity
    {
        public DateTimeOffset StartTime { get; set; }
        //public DateTimeOffset EndTime { get; set; }

        [ForeignKey("Hall")]
        public Guid CinemaHallId { get; set; }
        public Hall CinemaHall { get; set; } = default!;
        //public string HallName { get; set; } = default!;
        //public string CinemaName { get; set; } = default!;

        [ForeignKey("Movie")]
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = default!;
        //public string MovieTitle { get; set; } = default!;
        //public long MovieRuntimeMinutes { get; set; }
		//public string MoviePosterImage { get; set; } = default!;
	}
}