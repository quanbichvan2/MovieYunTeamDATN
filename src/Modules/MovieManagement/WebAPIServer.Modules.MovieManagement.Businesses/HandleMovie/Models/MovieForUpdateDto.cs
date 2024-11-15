using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models.Base;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models
{
    public class MovieForUpdateDto: MovieBaseDto
    {
        public IList<MovieCastMemberForUpdateDto> CastMembers { get; set; } = new List<MovieCastMemberForUpdateDto>();
        public IList<MovieGenreForUpdateDto> Genres { get; set; } = new List<MovieGenreForUpdateDto>();
    }
    public class MovieCastMemberForUpdateDto
    {
        public Guid? Id { get; set; }
    }
    public class MovieGenreForUpdateDto
    {
        public Guid? Id { get; set; }
    }
}
