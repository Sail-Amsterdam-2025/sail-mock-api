namespace Sail_MockApi.Api.DTOs.TimeBlocks
{
    public class UpdateTimeblockDTO
    {
        public string Id { get; set; } 
        public string? GroupId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? LocationId { get; set; }
    }
}
