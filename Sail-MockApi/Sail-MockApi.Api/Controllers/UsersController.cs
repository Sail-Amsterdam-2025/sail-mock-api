using Microsoft.AspNetCore.Mvc;
using Sail_MockApi.Api.DTOs.Users;
using Sail_MockApi.Api.Services;

namespace Sail_MockApi.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly CheckinService _checkinService;

        // Uncomment the following line to require authentication for all UsersController routes in every method
        /*if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }*/

        public UsersController(UserService userService, CheckinService checkinService)
        {
            _userService = userService;
            _checkinService = checkinService;
        }


        [HttpGet]
        public IActionResult GetAll(
            [FromQuery] int limit = 100,
            [FromQuery] int offset = 0,
            [FromQuery] string firstName = null,
            [FromQuery] string lastName = null,
            [FromQuery] string email = null,
            [FromQuery] int? roleId = null,
            [FromQuery] int? groupId = null,
            [FromQuery] string volunteerId = null)
        {
            if (limit < 1 || limit > 1000 || offset < 0)
            {
                return BadRequest("Limit must be between 1 and 1000, and offset must be zero or greater.");
            }

            /*if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }*/

            var users = _userService.GetAllUsers(limit, offset, firstName, lastName, email, roleId, groupId, volunteerId);

            if (users == null || !users.Any())
            {
                return NotFound("No users found with the specified criteria.");
            }

            bool someFeatureNotImplemented = false;
            if (someFeatureNotImplemented)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, "Feature not implemented.");
            }

            return Ok(users);
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById(string userId)
        {
            if (userId == null)
            {
                return BadRequest("Invalid user ID.");
            }

            /*if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }*/

            var user = _userService.GetById(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] NewUserDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid input. Ensure all required fields are provided.");
            }

            /*if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }*/

            // Create a new user based on the provided data
            var newUser = _userService.Add(userDto);


            // Return the created user with a 201 Created response
            return CreatedAtAction(nameof(CreateUser), new { userId = newUser.Guid }, newUser);
        }

        [HttpPatch("{userId}")]
        public IActionResult UpdateUser(string userId, [FromBody] UserPatchDTO userPatchDto)
        {
            if (userId == null || userPatchDto == null)
            {
                return BadRequest("Invalid input. Ensure all required fields are provided.");
            }

            /*if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }*/

            // Update the user with the provided data
            var updatedUser = _userService.Update(userPatchDto, userId);

            if (updatedUser == null)
            {
                return NotFound("User not found.");
            }

            return Ok(updatedUser);
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(string userId)
        {
            if (userId == null)
            {
                return BadRequest("Invalid user ID.");
            }

            /*if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }*/

            _userService.Delete(userId);

            return NoContent();
        }

        [HttpPost("forgotpassword")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDto)
        {
            if (forgotPasswordDto == null)
            {
                return BadRequest("Invalid input. Ensure all required fields are provided.");
            }

            /*if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }*/

            // Send a password reset email to the user
            var success = _userService.SendPasswordResetEmail(forgotPasswordDto.Email);

            if (!success)
            {
                return NotFound("User not found.");
            }

            return Ok("Email is sent to user.");
        }

        [HttpPost("resetpassword")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDTO resetPasswordDto)
        {
            if (resetPasswordDto == null)
            {
                return BadRequest("Invalid input. Ensure all required fields are provided.");
            }

            /*if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }*/

            // Reset the user's password
            var success = _userService.ResetPassword(resetPasswordDto);

            if (!success)
            {
                return NotFound("User not found.");
            }

            return Ok("Reset password successfully!");
        }


        [HttpGet("checkin")]
        public IActionResult GetAllCheckins(
            [FromQuery] int limit = 100,
            [FromQuery] int offset = 0,
            [FromQuery] int? groupId = null,
            [FromQuery] int? roleId = null)
        {
            if (limit < 1 || limit > 1000 || offset < 0)
            {
                return BadRequest("Limit must be between 1 and 1000, and offset must be zero or greater.");
            }

            /*if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }*/

            var checkins = _checkinService.GetAllCheckins(limit, offset, groupId, roleId);

            if (checkins == null || !checkins.Any())
            {
                return NotFound("No check-ins found with the specified criteria.");
            }

            return Ok(checkins);
        }

        [HttpGet("checkin/{userId}")]
        public IActionResult GetCheckinById(string userId)
        {
            if (userId == null)
            {
                return BadRequest("Invalid user ID.");
            }

            /*if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }*/

            var checkin = _checkinService.GetById(userId);

            if (checkin == null)
            {
                return NotFound("Check-in not found.");
            }

            return Ok(checkin);
        }

        [HttpPost("checkin")]
        public IActionResult CreateCheckin([FromBody] NewCheckinDTO newCheckin)
        {
            if (newCheckin == null)
            {
                return BadRequest("Invalid input. Ensure all required fields are provided.");
            }

            /*if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }*/

            var checkin = _checkinService.Add(newCheckin);
            return CreatedAtAction(nameof(CreateCheckin), new { userId = checkin.userId }, checkin);
        }

        [HttpDelete("checkin/{userId}")]
        public IActionResult DeleteCheckin(string userId)
        {
            if (userId == null)
            {
                return BadRequest("Invalid user ID.");
            }

            /*if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }*/

            _checkinService.Delete(userId);

            return NoContent();
        }

        [HttpPost("/api/Users/auth/login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("Invalid input. Ensure all required fields are provided.");
            }

            var user = _userService.Login(loginDto);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            Response.Headers.Add("Authorization", $"Bearer {user.AccessToken}");

            return Ok(user);
        }

        [HttpPost("/api/Users/auth/refresh")]
        public IActionResult RefreshToken()
        {
            //For if you have the header
            /*if (!Request.Headers.TryGetValue("Authorization", out var authorizationHeader) || string.IsNullOrWhiteSpace(authorizationHeader))
            {
                return BadRequest("Authorization token is missing from the headers.");
            }

            var token = authorizationHeader.ToString().Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase).Trim();*/
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            var user = _userService.Refresh(token);
            if (user == null)
            {
                return Unauthorized("Invalid or expired token.");
            }

            var newAccessToken = user[0];

            var newRefreshToken = user[1];

            Response.Headers.Add("Authorization", $"Bearer {newAccessToken}");
            Response.Headers.Add("X-Refresh-Token", newRefreshToken); 

            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken // Optional, only if issuing a new refresh token
            });

        }

        [HttpPost("/api/Users/auth/register")]
        public IActionResult Register([FromBody] NewRegisterDTO newUserDto)
        {
            if (newUserDto == null)
            {
                return BadRequest("Invalid input. Ensure all required fields are provided.");
            }

            var user = _userService.Register(newUserDto);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(new {guiId = user.Guid });
        }
        
    }
}
