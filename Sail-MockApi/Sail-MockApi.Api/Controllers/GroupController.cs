using Microsoft.AspNetCore.Mvc;
using Sail_MockApi.Api.DTOs;
using Sail_MockApi.Api.Services;
using System.Text.RegularExpressions;

namespace Sail_MockApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {

        private readonly GroupService _groupService;

        public GroupController(GroupService groupService)
        {
            _groupService = groupService;
        }



        // GET: api/<ExampleController>
        [HttpGet]
        public IActionResult Get([FromQuery] int limit = 10, [FromQuery] int offset = 0, [FromQuery] string name = null, [FromQuery] string? roleId = null, [FromQuery] string? teamLeaderId = null)
        {
            var allGroups = _groupService.GetAllGroups(limit, offset, name, roleId, teamLeaderId);

            if (allGroups.Count == 0)
            {
                return BadRequest("No groups found.");
            }

            return Ok(allGroups);
        }

        // GET api/<ExampleController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            GroupResponseDto groupResponse = _groupService.GetGroupById(id);

            if (groupResponse == null)
            {
                return NotFound($"Group with ID {id} not found.");
            }

            return new OkObjectResult(groupResponse);
        }

        // POST api/<ExampleController>
        [HttpPost]
        public IActionResult Post([FromBody] GroupRequestDto groupRequest)
        {
            if (groupRequest == null)
            {
                return BadRequest("Group request data is required.");
            }

            var groupResponse = _groupService.AddGroup(groupRequest);

            if (groupResponse == null)
            {
                return BadRequest("Failed to add the group.");
            }

            return CreatedAtAction(nameof(Post), new { id = groupResponse.Id }, groupResponse);
        }

        // PUT api/<ExampleController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] GroupRequestDto updatedGroup)
        {
            if (updatedGroup == null)
            {
                return BadRequest("Invalid data.");
            }
            var updatedGroupResponse = _groupService.PatchGroupById(id, updatedGroup);

            if (updatedGroupResponse == null)
            {
                return NotFound($"Group with ID {id} not found.");
            }
            return Ok(updatedGroupResponse);
        }

        // DELETE api/<ExampleController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {

            bool deleted = _groupService.DeleteGroupById(id);
            if (!deleted)
            {
                return BadRequest("Group not found");
            }
            return NoContent();
        }
    }
}
