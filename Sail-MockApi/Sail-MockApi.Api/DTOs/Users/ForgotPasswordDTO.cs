namespace Sail_MockApi.Api.DTOs.Users
{
    public class ForgotPasswordDTO
    {
        public string Email { get; set; }

        public ForgotPasswordDTO(string email)
        {
            Email = email;
        }

        public ForgotPasswordDTO()
        {
        }
    }
}