using FluentValidation;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Validations
{
    public class ShowForUpdateDtoValidation: AbstractValidator<ShowForUpdateDto>
    {
        public ShowForUpdateDtoValidation()
        {
            
        }
    }
}
