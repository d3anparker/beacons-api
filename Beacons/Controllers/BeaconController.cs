using Beacons.Data;
using Beacons.Models.Requests;
using Beacons.Services.Beacons;
using Microsoft.AspNetCore.Mvc;

namespace Beacons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeaconController : ControllerBase
    {   
        private readonly IBeaconService _beaconService;

        public BeaconController(IBeaconService beaconService)
        {
            _beaconService = beaconService;
        }

        [HttpGet("{id:guid}", Name = "GetBeaconById")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var beacon = await _beaconService.GetBeaconByIdAsync(id);

            if(beacon is null)
            {
                return NotFound();
            }

            return Ok(beacon);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]BeaconCreateRequest request)
        {
            var model = new Beacon()
            {
                Longitude = request.Longitude,
            };
            
            await _beaconService.CreateBeaconAsync(model);

            return CreatedAtRoute("GetBeaconById", new { id = model.Id }, model);
        } 
    }
}
