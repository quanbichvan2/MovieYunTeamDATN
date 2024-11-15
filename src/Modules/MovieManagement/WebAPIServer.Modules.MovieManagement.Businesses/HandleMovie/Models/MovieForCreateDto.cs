using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models.Base;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models
{
    public class MovieForCreateDto: MovieBaseDto
    {
        public IList<MovieCastMemberForCreateDto> CastMembers { get; set; } = new List<MovieCastMemberForCreateDto>();
        public IList<MovieGenreForCreateDto> Genres { get; set; } = new List<MovieGenreForCreateDto>();
    }
    public class MovieCastMemberForCreateDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
    }
    public class MovieGenreForCreateDto
    {
        public Guid? Id { get; set; }
    }
}