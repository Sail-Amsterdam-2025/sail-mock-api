﻿namespace Sail_MockApi.Api.DTOs
{
    public class LocationResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public List<CoordinateResponseDto> Coordinates { get; set; } = new List<CoordinateResponseDto>();

        public LocationResponseDto(string id, string name, string type, string color, List<CoordinateResponseDto> coordinates) {
            
            Id = id;
            Name = name;
            Type = type;
            Color = color;
            Coordinates = coordinates;
        }
        public LocationResponseDto() { }
        

    }
}
