using Microsoft.AspNetCore.Mvc;

namespace WebAPIServer.Modules.Payment.Api.Controllers
{

    [ApiController]
    [Route(BasePath + "/[controller]")]
    internal abstract class BaseController : ControllerBase
    {
        protected const string BasePath = "payment-module";
    }
}
