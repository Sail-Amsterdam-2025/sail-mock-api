namespace Sail_MockApi.Api.DTOs.TimeBlocks
{
    public class TimeblockResponseDTO
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid LocationId { get; set; }
    }
}
