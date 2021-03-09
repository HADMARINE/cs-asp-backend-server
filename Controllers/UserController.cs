
using cs_asp_backend_server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace cs_asp_backend_server.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public string Get()
        {
            return "Hello World";
            // return CreatedAtAction(nameof(GetById));
        }

        [HttpGet("user_many")]
        public User GetUser()
        {
            return new User();
        }

        [HttpGet("test")]
        public object GetAny()
        {
            return new
            {
                a = 1,
                b = 2,
                c = 3,
                d = 4,
                e = "fuck"
            };
        }
    }
}