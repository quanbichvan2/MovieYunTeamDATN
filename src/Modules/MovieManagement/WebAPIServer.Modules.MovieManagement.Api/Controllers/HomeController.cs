using Microsoft.AspNetCore.Mvc;

namespace WebAPIServer.Modules.MovieManagement.Api.Controllers
{
    [Route(BasePath)]
    internal class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Movie Management module");
        }
    }
}
