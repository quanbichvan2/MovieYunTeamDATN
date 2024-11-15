using Microsoft.AspNetCore.Mvc;

namespace WebAPIServer.Modules.Booking.Api.Controllers
{
	[Route(BasePath)]
	internal class HomeController : BaseController
	{
		[HttpGet]
		public IActionResult Get()
		{
			return Ok("Booking module");
		}
	}
}
