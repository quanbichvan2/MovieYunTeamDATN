using FluentValidation;
using WebAPIServer.Modules.Identity.Businesses.Models;

namespace WebAPIServer.Modules.Identity.Businesses.Validations
{
	public class RoleValidation: AbstractValidator<RoleForCommand>
	{
        public RoleValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Không để thuộc tính trống")
                .NotNull().WithMessage("Không để thuộc tính trống");
		}
    }
}
