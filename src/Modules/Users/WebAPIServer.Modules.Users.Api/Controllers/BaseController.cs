using Microsoft.AspNetCore.Mvc;

namespace WebAPIServer.Modules.Users.Api.Controllers
{
	[ApiController]
	[Route(BasePath + "/[controller]")]
	internal abstract class BaseController : ControllerBase
	{
		protected const string BasePath = "users-module";
	}
}
