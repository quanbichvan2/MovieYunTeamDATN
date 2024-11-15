using Microsoft.AspNetCore.Mvc;

namespace WebAPIServer.Modules.Vouchers.Api.Controllers
{
	[Route(BasePath)]
	internal class HomeController : BaseController
	{
		[HttpGet]
		public IActionResult Get()
		{
			return Ok("Voucher module");
		}
	}
}
