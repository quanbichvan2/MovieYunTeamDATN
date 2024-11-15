using FluentValidation;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Validations
{
    public class ShowForCreateDtoValidation: AbstractValidator<ShowForCreateDto>
    {
        public ShowForCreateDtoValidation()
        {
            
        }
    }
}