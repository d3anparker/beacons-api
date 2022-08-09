using Microsoft.AspNetCore.Mvc;

namespace Beacons.Controllers
{
    [Route("[controller]"), ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Healthy");
        }
    }
}
