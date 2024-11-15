using Microsoft.AspNetCore.Mvc;

namespace WebAPIServer.Modules.Tickets.Api.Controllers
{
    [Route(BasePath)]
    internal class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("TicketTypes module");
        }
    }
}