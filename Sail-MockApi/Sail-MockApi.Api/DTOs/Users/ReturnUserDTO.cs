namespace Sail_MockApi.Api.DTOs.Users
{
    public class ReturnUserDTO
    {
        public string Guid { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string VolunteerId { get; set; }
        public int RoleId { get; set; }
        public int GroupId { get; set; }


        public ReturnUserDTO(string guid, string email, string firstName, string lastName, string volunteerId, int roleId, int groupId)
        {
            Guid = guid;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            VolunteerId = volunteerId;
            RoleId = roleId;
            GroupId = groupId;
        }

        public ReturnUserDTO()
        {
        }

    }



}
