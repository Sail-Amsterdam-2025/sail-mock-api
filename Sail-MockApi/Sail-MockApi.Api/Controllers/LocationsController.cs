using Microsoft.AspNetCore.Mvc;
using Sail_MockApi.Api.DTOs;
using Sail_MockApi.Api.Services;

namespace Sail_MockApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {

        private readonly LocationService _locationService;

        public LocationsController(LocationService locationService)
        {
            _locationService = locationService;
        }



        // GET: api/<ExampleController>
        [HttpGet]
        public IActionResult Get([FromQuery] int limit = 10, [FromQuery] int offset = 0, [FromQuery] string name = null)
        {
            var allLocations = _locationService.GetAllLocations(limit, offset, name);

            if (allLocations.Count == 0)
            {
                return BadRequest("No roles found.");
            }

            return Ok(allLocations);
        }

        // GET api/<ExampleController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            LocationResponseDto locationResponse = _locationService.GetLocationById(id);

            if (locationResponse == null)
            {
                return NotFound($"Location with ID {id} not found.");
            }

            return new OkObjectResult(locationResponse);
        }

        // POST api/<ExampleController>
        [HttpPost]
        public IActionResult Post([FromBody] LocationRequestDto locationRequest)
        {
            if (locationRequest == null)
            {
                return BadRequest("Location request data is required.");
            }

            var locationResponse = _locationService.AddLocation(locationRequest);

            if (locationResponse == null)
            {
                return BadRequest("Failed to add the location.");
            }

            return CreatedAtAction(nameof(Post), new { id = locationResponse.Id }, locationResponse);
        }

        // PUT api/<ExampleController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] LocationRequestDto updatedLocation)
        {
            if (updatedLocation == null)
            {
                return BadRequest("Invalid data.");
            }
            var updatedLocationResponse = _locationService.PatchLocationById(id, updatedLocation);

            if (updatedLocationResponse == null)
            {
                return NotFound($"Location with ID {id} not found.");
            }
            return Ok(updatedLocationResponse);
        }

        // DELETE api/<ExampleController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {

            bool deleted = _locationService.DeleteLocationById(id);
            if (!deleted)
            {
                return BadRequest("Location not found");
            }
            return NoContent();
        }
    }
}
