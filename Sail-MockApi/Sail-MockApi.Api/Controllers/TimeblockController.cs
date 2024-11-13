using Microsoft.AspNetCore.Mvc;
using Sail_MockApi.Api.DTOs.TimeBlocks;
using Sail_MockApi.Api.Services;

namespace Sail_MockApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeblockController : ControllerBase
    {
        private TimeblockService _timeblockService;

        public TimeblockController(TimeblockService timeblockService)
        {
            _timeblockService = timeblockService;
        }

        [HttpPost]
        public IActionResult AddTimeblock([FromBody] TimeblockRequestDTO timeblockRequestDTO)
        {
            try { 
            

                // Call the service to add the timeblock. You would likely have a service method for this logic.
                var createdTimeblockResponseDTO = _timeblockService.AddTimeblock(timeblockRequestDTO);

                // Return a 201 Created response with the TimeblockResponseDTO
                return CreatedAtAction(nameof(GetTimeblockById), new { timeblockId = createdTimeblockResponseDTO.Id }, createdTimeblockResponseDTO);

            }
            catch (Exception ex)
            {
                // Return a 400 Bad Request if any error occurs
                return BadRequest(new { message = "An error occurred while adding the timeblock.", details = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetTimeblocks([FromQuery] int? limit, [FromQuery] int? offset, [FromQuery] int? groupId,
                                    [FromQuery] DateTime? startTime, [FromQuery] DateTime? endTime)
        {
            try
            {
                // Set default values if not provided
                limit = limit ?? 50;
                offset = offset ?? 0;

                // Retrieve timeblocks (you could apply filtering based on the provided query params)
                var timeblocks = _timeblockService.GetTimeblocks(limit.Value, offset.Value, groupId, startTime, endTime);

                // Return the list of timeblocks with status code 200
                return Ok(timeblocks);
            }
            catch (Exception ex)
            {
                // Return an error response in case of an exception
                return StatusCode(500, new { message = "An error occurred while retrieving timeblocks.", details = ex.Message });
            }
        }

        [HttpPatch("{timeblockId}")]
        public IActionResult UpdateTimeblock(int timeblockId, [FromBody] UpdateTimeblockDTO updateTimeblock)
        {
            try
            {
                // Fetch the existing timeblock by ID
                var timeblock = _timeblockService.GetTimeblockById(timeblockId);

                if (timeblock == null)
                {
                    return NotFound(new { message = "Timeblock not found" });
                }

                // Update the properties of the timeblock based on the provided DTO
                if (updateTimeblock.GroupId.HasValue)
                {
                    timeblock.GroupId = updateTimeblock.GroupId.Value;
                }
                if (updateTimeblock.StartTime.HasValue)
                {
                    timeblock.StartTime = updateTimeblock.StartTime.Value;
                }
                if (updateTimeblock.EndTime.HasValue)
                {
                    timeblock.EndTime = updateTimeblock.EndTime.Value;
                }
                if (updateTimeblock.LocationId.HasValue)
                {
                    timeblock.LocationId = updateTimeblock.LocationId.Value;
                }

                // Save the updated timeblock (this might involve saving to a database)
                _timeblockService.UpdateTimeblock(timeblock);

                // Return the updated timeblock as a response
                var timeblockResponseDTO = new TimeblockResponseDTO
                {
                    Id = timeblock.Id,
                    GroupId = timeblock.GroupId,
                    StartTime = timeblock.StartTime,
                    EndTime = timeblock.EndTime,
                    LocationId = timeblock.LocationId
                };

                return Ok(timeblockResponseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while updating the timeblock.", details = ex.Message });
            }
        }

        [HttpGet("{timeblockId}")]
        public IActionResult GetTimeblockById(int timeblockId)
        {
            try
            {
                // Fetch the timeblock by ID
                var timeblock = _timeblockService.GetTimeblockById(timeblockId);

                if (timeblock == null)
                {
                    return NotFound(new { message = "Timeblock not found" });
                }

                // Map the found timeblock to a response DTO
                var timeblockResponseDTO = new TimeblockResponseDTO
                {
                    Id = timeblock.Id,
                    GroupId = timeblock.GroupId,
                    StartTime = timeblock.StartTime,
                    EndTime = timeblock.EndTime,
                    LocationId = timeblock.LocationId
                };

                // Return the timeblock data in the response
                return Ok(timeblockResponseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while retrieving the timeblock.", details = ex.Message });
            }
        }

        [HttpDelete("{timeblockId}")]
        public IActionResult DeleteTimeblock(int timeblockId)
        {
            try
            {
                // Fetch the timeblock to check if it exists
                var timeblock = _timeblockService.GetTimeblockById(timeblockId);

                if (timeblock == null)
                {
                    // Return 404 if timeblock doesn't exist
                    return NotFound(new { message = "Timeblock not found" });
                }

                // Proceed to delete the timeblock
                var isDeleted = _timeblockService.DeleteTimeblock(timeblockId);

                if (!isDeleted)
                {
                    return BadRequest(new { message = "An error occurred while trying to delete the timeblock." });
                }

                // Return 204 (No Content) on successful deletion
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while deleting the timeblock.", details = ex.Message });
            }
        }


    }
}
