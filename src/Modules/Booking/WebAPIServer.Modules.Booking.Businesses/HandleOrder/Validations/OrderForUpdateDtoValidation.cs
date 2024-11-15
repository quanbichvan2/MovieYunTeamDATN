using FluentValidation;
using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models;

namespace WebAPIServer.Modules.Booking.Businesses.HandleOrder.Validations
{
	public class OrderForUpdateDtoValidation : AbstractValidator<OrderForUpdateDto>
	{
		public OrderForUpdateDtoValidation()
		{

		}
	}
}
