using FluentValidation;
using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models;

namespace WebAPIServer.Modules.Booking.Businesses.HandleOrder.Validations
{
	public class OrderForCreateDtoValidation : AbstractValidator<OrderForCreateDto>
	{
		public OrderForCreateDtoValidation()
		{
		}
	}
}
