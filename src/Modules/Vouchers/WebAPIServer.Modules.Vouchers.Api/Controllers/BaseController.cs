using Microsoft.AspNetCore.Mvc;

namespace WebAPIServer.Modules.Vouchers.Api.Controllers
{
	[ApiController]
	[Route(BasePath + "/[controller]")]
	internal abstract class BaseController : ControllerBase
	{
		protected const string BasePath = "Voucher-module";
	}
}
