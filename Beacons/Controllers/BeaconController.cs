using Beacons.Data;
using Beacons.Mapping;
using Beacons.Models;
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
        private readonly IBeaconMapper _beaconMapper;

        public BeaconController(IBeaconService beaconService, IBeaconMapper beaconMapper)
        {
            _beaconService = beaconService;
            _beaconMapper = beaconMapper;
        }

        [HttpGet("{id:guid}", Name = "GetBeaconById")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var beacon = await _beaconService.GetBeaconByIdAsync(id);

            if(beacon is null)
            {
                return NotFound();
            }

            var model = _beaconMapper.MapToModel(beacon);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]BeaconCreateRequest request)
        {
            var model = _beaconMapper.MapToEntity(request.Beacon);            
            var response = await _beaconService.CreateBeaconAsync(model);

            if(!response.Success)
            {
                return BadRequest(response.Errors);
            }

            var responseModel = _beaconMapper.MapToModel(response.Data);

            return CreatedAtRoute("GetBeaconById", new { id = response.Data.Id }, responseModel);
        } 
    }
}
