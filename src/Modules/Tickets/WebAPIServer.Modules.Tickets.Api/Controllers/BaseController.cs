using Microsoft.AspNetCore.Mvc;

namespace WebAPIServer.Modules.Tickets.Api.Controllers
{
    [ApiController]
    [Route(BasePath + "/[controller]")]
    internal abstract class BaseController : ControllerBase
    {
        protected const string BasePath = "tickets-module";
    }
}