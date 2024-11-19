using Sail_MockApi.Api.DTOs;
using Sail_MockApi.Api.DTOs.Users;

namespace Sail_MockApi.Api.Services
{
    public class UserService 
    {
        private readonly List<ReturnUserDTO> _users;
        
            
        public UserService() 
        {
            _users = new List<ReturnUserDTO>
            {
                new ReturnUserDTO { Guid = Guid.NewGuid().ToString(), Email = "Henk@gmail.com", FirstName = "Henk", LastName = "Jansen", VolunteerId = "1234", RoleId = 1, GroupId = 1 },
                new ReturnUserDTO { Guid = Guid.NewGuid().ToString(), Email = "Karel@gmail.com", FirstName = "Karel", LastName = "Jansen", VolunteerId = "1235", RoleId = 2, GroupId = 2 },
                new ReturnUserDTO { Guid = Guid.NewGuid().ToString(), Email = "Willemijn@gmail.com", FirstName = "Willemijn", LastName = "Jansen", VolunteerId = "1236", RoleId = 3, GroupId = 3 },
            };

        }

        public IEnumerable<ReturnUserDTO> GetAllUsers(
            int limit = 100,
            int offset = 0,
            string firstName = null,
            string lastName = null,
            string email = null,
            int? roleId = null,
            int? groupId = null,
            string volunteerId = null)
        {
            var query = _users.AsQueryable();

            if (!string.IsNullOrEmpty(firstName))
                query = query.Where(u => u.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(lastName))
                query = query.Where(u => u.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (roleId.HasValue)
                query = query.Where(u => u.RoleId == roleId.Value);

            if (groupId.HasValue)
                query = query.Where(u => u.GroupId == groupId.Value);

            if (!string.IsNullOrEmpty(volunteerId))
                query = query.Where(u => u.VolunteerId.Equals(volunteerId, StringComparison.OrdinalIgnoreCase));

            return query
                .Skip(offset)
                .Take(limit)
                .ToList();
        }

        public ReturnUserDTO? GetById(string guid)
        {
            return _users.FirstOrDefault(u => u.Guid.Equals(guid));
        }
        

        public ReturnUserDTO Add(NewUserDTO user)
        {
            ReturnUserDTO returnUserDTO = new ReturnUserDTO
            {
                Guid = Guid.NewGuid().ToString(),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                VolunteerId = user.VolunteerId,
                RoleId = user.RoleId,
                GroupId = user.GroupId
            };
            _users.Add(returnUserDTO);
            return returnUserDTO;
        }

        public ReturnUserDTO? Register(NewRegisterDTO user)
        {
            ReturnUserDTO returnUserDTO = new ReturnUserDTO
            {
                Guid = Guid.NewGuid().ToString(),
                Email = user.Email,
                VolunteerId = user.VolunteerId
            };
            _users.Add(returnUserDTO);
            return returnUserDTO;
        }

        public ReturnUserDTO? Update(UserPatchDTO userPatchDto, string id)
        {
            // Find the existing user by ID
            var existingUser = _users.FirstOrDefault(u => u.Guid == id);
            if (existingUser == null) return null;

            // Update only the fields that are not null in the UserPatchDto
            if (!string.IsNullOrEmpty(userPatchDto.FirstName))
                existingUser.FirstName = userPatchDto.FirstName;

            if (!string.IsNullOrEmpty(userPatchDto.LastName))
                existingUser.LastName = userPatchDto.LastName;

            if (!string.IsNullOrEmpty(userPatchDto.Email))
                existingUser.Email = userPatchDto.Email;

            if (!string.IsNullOrEmpty(userPatchDto.VolunteerId))
                existingUser.VolunteerId = userPatchDto.VolunteerId;

            if (userPatchDto.RoleId.HasValue)
                existingUser.RoleId = userPatchDto.RoleId.Value;

            if (userPatchDto.GroupId.HasValue)
                existingUser.GroupId = userPatchDto.GroupId.Value;

            return existingUser;
        }


        public void Delete(string id)
        {
            var user = _users.FirstOrDefault(u => u.Guid == id);
            if (user != null)
            {
                _users.Remove(user);
            }
        }

        public bool SendPasswordResetEmail(string email)
        {
            return true;
        }

        public bool ResetPassword(ResetPasswordDTO resetPasswordDto)
        {
            return true;
        }

        public LoginResponseDto Login(LoginDTO loginDto)
        {
            return new LoginResponseDto();
        }

        public string[] Refresh(string token)
        {
            return new string[] { "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c" };
        }
    }
}
