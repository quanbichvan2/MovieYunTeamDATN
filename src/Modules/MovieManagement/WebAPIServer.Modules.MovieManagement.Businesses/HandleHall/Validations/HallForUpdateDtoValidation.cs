using FluentValidation;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Validations
{
    public class HallForUpdateDtoValidation : AbstractValidator<HallForUpdateDto>
    {
        public HallForUpdateDtoValidation()
        {
            
        }
    }
}
