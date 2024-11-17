using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models.Base;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models
{
    public class ShowForCreateDto: ShowBaseDto
    {
        public DateTimeOffset StartTime { get; set; }
        public Guid CinemaHallId { get; set; }
    }
}
