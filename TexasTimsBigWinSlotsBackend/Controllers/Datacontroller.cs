//This controller was just a proof of concept
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TexasTimsBigWinSlots.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "Hello World!" });
        }
    }
}
