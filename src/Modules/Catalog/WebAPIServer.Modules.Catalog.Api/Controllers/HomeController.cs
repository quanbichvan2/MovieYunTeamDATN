using Microsoft.AspNetCore.Mvc;

namespace WebAPIServer.Modules.Catalog.Api.Controllers
{
    [Route(BasePath)]
    internal class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Catalog module");
        }
    }
}