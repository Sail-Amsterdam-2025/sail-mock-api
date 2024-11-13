using Microsoft.AspNetCore.Mvc;
using Sail_MockApi.Api.DTOs;
using Sail_MockApi.Api.Services;

namespace Sail_MockApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {

        private readonly MapService _mapService;

        public MapController(MapService mapService)
        {
            _mapService = mapService;
        }



        // GET: api/<ExampleController>
        [HttpGet]
        public IActionResult Get()
        {
            MapResponseDto map = _mapService.GetMap();

            if (map == null)
            {
                return BadRequest("No map found.");
            }

            return Ok(map);
        }

        // POST api/<ExampleController>
        [HttpPost]
        public IActionResult Post([FromBody] MapRequestDto mapRequest)
        {
            if (mapRequest == null)
            {
                return BadRequest("Map request data is required.");
            }

            var mapResponse = _mapService.AddMap(mapRequest);

            if (mapResponse == null)
            {
                return BadRequest("Failed to add the map.");
            }

            return Created("/api/map", mapResponse);
        }

        // PUT api/<ExampleController>/5
        [HttpPut]
        public IActionResult Put([FromBody] MapRequestDto updatedMap)
        {
            if (updatedMap == null)
            {
                return BadRequest("Invalid data.");
            }
            var updatedMapResponse = _mapService.PatchMap(updatedMap);

            if (updatedMapResponse == null)
            {
                return NotFound($"Map not found.");
            }
            return Ok(updatedMapResponse);
        }
    }
}
