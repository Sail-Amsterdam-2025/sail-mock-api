namespace Sail_MockApi.Api.DTOs
{
    public class LocationResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<CoordinateResponseDto> Coordinates { get; set; } = new List<CoordinateResponseDto>();

        public LocationResponseDto(string id, string name, List<CoordinateResponseDto> coordinates) {
            
            Id = id;
            Name = name;
            Coordinates = coordinates;
        }
        public LocationResponseDto() { }
        

    }
}
