using cs_asp_backend_server.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace cs_asp_backend_server.Controllers
{
    [ApiController]
    [Route("")]
    public class RootController : ControllerBase
    {
        [HttpGet]
        public object GetRoot()
        {
            return new
            {
                version = Constants.ApiVersion,
                apidoc = "/docs/api"
            };
        }
    }
}