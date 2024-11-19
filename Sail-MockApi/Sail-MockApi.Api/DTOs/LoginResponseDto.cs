namespace Sail_MockApi.Api.DTOs
{
    public class LoginResponseDto
    {
        public string Id { get; set; }
        public string Fristname { get; set; }
        public string Lastname { get; set; }
        public string RefreshToken  { get; set; }
        public string AccessToken { get; set; }

        public LoginResponseDto() {

            Id = Guid.NewGuid().ToString();
            Fristname = "Karel";
            Lastname = "van den Berg";
            RefreshToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiIxMjM0NTY3ODkwIiwidHlwZSI6InJlZnJlc2giLCJleHBpcmF0aW9uVGltZSI6MTcwMDEwNDAwMH0.4QrfdTB5c_Vg8zfq2RT9RpMz18XH7JJsEw0tCCyG1sE\r\n";
            AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
        }

    }
}
