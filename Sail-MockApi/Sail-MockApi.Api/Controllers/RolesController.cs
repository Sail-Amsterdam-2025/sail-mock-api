using Microsoft.AspNetCore.Mvc;
using Sail_MockApi.Api.DTOs;
using Sail_MockApi.Api.Services;

namespace Sail_MockApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        private readonly RoleService _roleService;

        public RolesController(RoleService roleService)
        {
            _roleService = roleService;
        }



        // GET: api/<ExampleController>
        [HttpGet]
        public IActionResult Get([FromQuery] int limit = 10, [FromQuery] int offset = 0, [FromQuery] string name = null)
        {
            var allRoles = _roleService.GetAllRoles(limit, offset, name);

            if (allRoles.Count == 0)
            {
                return BadRequest("No roles found.");
            }

            return Ok(allRoles);
        }

        // GET api/<ExampleController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            RoleResponseDto roleResponse = _roleService.GetRoleById(id);

            if (roleResponse == null)
            {
                return NotFound($"Role with ID {id} not found.");
            }

            return new OkObjectResult(roleResponse);
        }

        // POST api/<ExampleController>
        [HttpPost]
        public IActionResult Post([FromBody] RoleRequestDto roleRequest)
        {
            if (roleRequest == null)
            {
                return BadRequest("Group request data is required.");
            }

            var roleResponse = _roleService.AddRole(roleRequest);

            if (roleResponse == null)
            {
                return BadRequest("Failed to add the group.");
            }

            return CreatedAtAction(nameof(Post), new { id = roleResponse.Id }, roleResponse);
        }

        // PUT api/<ExampleController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] RoleRequestDto updatedRole)
        {
            if (updatedRole == null)
            {
                return BadRequest("Invalid data.");
            }
            var updatedRoleResponse = _roleService.PatchRoleById(id, updatedRole);

            if (updatedRoleResponse == null)
            {
                return NotFound($"Role with ID {id} not found.");
            }
            return Ok(updatedRoleResponse);
        }

        // DELETE api/<ExampleController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {

            bool deleted = _roleService.DeleteRoleById(id);
            if (!deleted)
            {
                return BadRequest("Role not found");
            }
            return NoContent();
        }
    }
}
