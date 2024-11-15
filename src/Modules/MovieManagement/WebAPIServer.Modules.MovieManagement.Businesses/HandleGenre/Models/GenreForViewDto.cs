using WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Models.Base;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Models
{
    public sealed class GenreForViewDto: DirectorBaseDto
    {
        public Guid Id { get; set; }
    }
}