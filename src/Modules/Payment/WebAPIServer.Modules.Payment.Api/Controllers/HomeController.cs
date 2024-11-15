using Microsoft.AspNetCore.Mvc;

namespace WebAPIServer.Modules.Payment.Api.Controllers
{
    [Route(BasePath)]
    internal class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Payment module");
        }
    }
}
