using FluentValidation;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Validations
{
    public class MovieForUpdateDtoValidation : AbstractValidator<MovieForUpdateDto>
    {
        public MovieForUpdateDtoValidation()
        {
            
        }
    }
}
