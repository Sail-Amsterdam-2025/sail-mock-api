namespace Sail_MockApi.Api.DTOs.Users
{
    public class ReturnCheckinDTO
    {
        public string userId { get; set; }
        public DateTime checkinTime { get; set; }

        public ReturnCheckinDTO(string userId, DateTime checkinTime)
        {
            this.userId = userId;
            this.checkinTime = checkinTime;
        }

        public ReturnCheckinDTO()
        {
        }
    }
}
