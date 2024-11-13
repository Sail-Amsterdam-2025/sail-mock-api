namespace Sail_MockApi.Api.DTOs.TimeBlocks
{
    public class UpdateTimeblockDTO
    {
        public int Id { get; set; } 
        public int? GroupId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? LocationId { get; set; }
    }
}
