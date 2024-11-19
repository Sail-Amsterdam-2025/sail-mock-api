namespace Sail_MockApi.Api.DTOs
{
    public class LocationRequestDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public List<CoordinateRequestDto> Coordinates { get; set; } = new List<CoordinateRequestDto>();
    }
}
