using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Api.Hello
{
    [Route("hello")]
    public class HelloController : Controller
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok("Hello World!");
        }
    }
}
