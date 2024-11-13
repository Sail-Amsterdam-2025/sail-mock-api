namespace Sail_MockApi.Api.DTOs.Users
{
    public class NewRegisterDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string VolunteerId { get; set; }

        public NewRegisterDTO(string email, string password, string volunteerdId)
        {
            Email = email;
            Password = password;
            VolunteerId = volunteerdId;
        }

        public NewRegisterDTO()
        {
        }
    }
}