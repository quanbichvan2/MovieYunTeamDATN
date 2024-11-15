using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Modules.MovieManagement.Domain.Enums;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models.Base
{
    public class MovieBaseDto
    {
        public string? Title { get; set; }
        public AgeRating? AgeRating { get; set; }
        public byte? RuntimeMinutes { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? TrailerLink { get; set; }
        public string? BannerText { get; set; }
        public string? HeaderImage { get; set; }
        public string? PosterImage { get; set; }
        public string? Description { get; set; }
        public Guid DirectorId { get; set; }

    }
}
