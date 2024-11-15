using FluentValidation;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Validations
{
    public class DirectorForCreateDtoValidation : AbstractValidator<DirectorForCreateDto>
    {
        public DirectorForCreateDtoValidation()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");
        }
    }
}
