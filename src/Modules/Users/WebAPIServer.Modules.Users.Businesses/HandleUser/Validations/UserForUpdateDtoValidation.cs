using FluentValidation;
using WebAPIServer.Modules.Users.Businesses.HandleUser.Models;

namespace WebAPIServer.Modules.Users.Businesses.HandleUser.Validations
{
	public class UserForUpdateDtoValidation : AbstractValidator<UserForUpdateDto>
	{
		public UserForUpdateDtoValidation()
		{
			RuleFor(x => x.Name)
				.NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
				.NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");
			RuleFor(x => x.Email)
				.NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
				.NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.")
				.Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("Định dạng {PropertyName} không hợp lệ.");
			RuleFor(x => x.PhoneNumber)
				.NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
				.NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.")
				.Matches(@"^(\+84|0)\d{9,10}$").WithMessage("Định dạng {PropertyName} không hợp lệ.");
		}
	}
}
