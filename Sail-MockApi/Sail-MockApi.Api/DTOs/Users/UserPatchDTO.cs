namespace Sail_MockApi.Api.DTOs.Users
{
    public class UserPatchDTO
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? VolunteerId { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        public int? GroupId { get; set; }

        public UserPatchDTO(string? email, string? firstName, string? lastName, string? volunteerId, string? password, int? roleId, int? groupId)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            VolunteerId = volunteerId;
            Password = password;
            RoleId = roleId;
            GroupId = groupId;
        }

        public UserPatchDTO()
        {
        }
    }
}
