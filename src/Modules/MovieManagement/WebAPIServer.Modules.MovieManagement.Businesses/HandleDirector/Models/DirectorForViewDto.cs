using WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Models.Base;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Models
{
    public sealed class DirectorForViewDto: DirectorBaseDto
    {
        public Guid Id { get; set; }
    }
}