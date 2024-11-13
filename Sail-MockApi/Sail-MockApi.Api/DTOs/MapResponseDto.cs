namespace Sail_MockApi.Api.DTOs
{
    public class MapResponseDto
    {
        public float Zoomlevel { get; set; }
        public CoordinateResponseDto StartCoordinate { get; set; }
        public List<LocationResponseDto> Locations { get; set; }

    }
}
