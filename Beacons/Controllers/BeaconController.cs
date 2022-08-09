using Beacons.Models;
using Microsoft.AspNetCore.Mvc;

namespace Beacons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeaconController : ControllerBase
    {
        [HttpGet("{id:guid}", Name = "GetBeaconById")]
        public Task<IActionResult> GetAsync(Guid id)
        {
            IActionResult result = Ok(new Beacon { Message = "Hello" });
            return Task.FromResult(result);
        }

        [HttpPost]
        public Task<IActionResult> CreateAsync([FromBody]BeaconCreateRequest request)
        {
            var model = new
            {
                Id = Guid.NewGuid()
            };

            IActionResult response = CreatedAtRoute("GetBeaconById", new { id = model.Id }, model);

            return Task.FromResult(response);
        } 
    }
}
