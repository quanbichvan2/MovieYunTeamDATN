using Microsoft.AspNetCore.Mvc;

namespace WebAPIServer.Modules.Catalog.Api.Controllers
{
    [ApiController]
    [Route(BasePath + "/[controller]")]
    internal abstract class BaseController: ControllerBase
    {
        protected const string BasePath = "catalog-module";
    }
}