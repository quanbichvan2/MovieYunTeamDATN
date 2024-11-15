using Microsoft.AspNetCore.Mvc;

namespace WebAPIServer.Modules.MovieManagement.Api.Controllers
{
    [ApiController]
    [Route(BasePath + "/[controller]")]
    internal abstract class BaseController : ControllerBase
    {
        protected const string BasePath = "movie-management-module";
    }
}