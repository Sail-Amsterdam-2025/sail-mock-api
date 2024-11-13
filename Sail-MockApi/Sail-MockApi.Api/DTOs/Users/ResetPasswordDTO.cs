namespace Sail_MockApi.Api.DTOs.Users
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public int Code { get; set; }
        public ResetPasswordDTO(string email, string newPassword, string confirmPassword,int code)
        {
            Email = email;
            NewPassword = newPassword;
            Code = code;
        }

        public ResetPasswordDTO()
        {
        }
    }
}