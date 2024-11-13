namespace Sail_MockApi.Api.DTOs.Users
{
    public class NewCheckinDTO
    {
        public string UserId { get; set; }

        public NewCheckinDTO(string userId)
        {
            UserId = userId;
        }

        public NewCheckinDTO()
        {
        }
    }
}
