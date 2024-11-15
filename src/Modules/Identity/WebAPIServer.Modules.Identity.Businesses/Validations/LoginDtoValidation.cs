using FluentValidation;
using WebAPIServer.Modules.Identity.Businesses.Models;

namespace WebAPIServer.Modules.Identity.Businesses.Validations
{
	public class LoginDtoValidation : AbstractValidator<LoginDto>
	{
		public LoginDtoValidation()
		{
			RuleFor(x => x.Email)
				.EmailAddress().WithMessage("Email của bạn chưa đúng định dạng vui lòng thử lại")
				.NotEmpty().WithMessage("Email của bạn không được để trống")
				.NotNull().WithMessage("Email của bạn không được để trống");
			RuleFor(x => x.Password)
				.NotNull().WithMessage("Mật khẩu của bạn không được để trống")
				.NotEmpty().WithMessage("Mật khẩu của bạn không được để trống.");
		}
	}
}
