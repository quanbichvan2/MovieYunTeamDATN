using Microsoft.AspNetCore.Mvc;

namespace WebAPIServer.Modules.Booking.Api.Controllers
{
	[ApiController]
	[Route(BasePath + "/[controller]")]
	internal abstract class BaseController : ControllerBase
	{
		protected const string BasePath = "booking-module";
	}
}
