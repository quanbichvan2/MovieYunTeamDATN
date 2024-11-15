namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models.Base
{
    public class ShowBaseDto
    {
        
        //public long Duration { get; set; }
        public Guid CinemaHallId { get; set; }
        public Guid MovieId { get; set; }
    }
}
