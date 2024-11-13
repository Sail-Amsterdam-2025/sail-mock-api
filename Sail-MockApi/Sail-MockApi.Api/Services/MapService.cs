using Sail_MockApi.Api.DTOs;
using System.Data;

namespace Sail_MockApi.Api.Services
{
    public class MapService
    {
        private static  MapResponseDto Map { get; set; } = new MapResponseDto();
        private readonly LocationService _locationService;
        
        public MapService(LocationService locationService) { 
            _locationService = locationService;
            AddDataToMap();
        }

        public void AddDataToMap()
        {

            // Ensure StartCoordinate is initialized before setting its properties
            if (Map.StartCoordinate == null)
            {
                Map.StartCoordinate = new CoordinateResponseDto();
                Map.StartCoordinate.Lat = 4.891140027255756;
                Map.StartCoordinate.Long = 52.40472178878887;
                Map.StartCoordinate.Id = Guid.NewGuid().ToString();
                Map.Zoomlevel = 20;
                Map.Locations = _locationService.GetAllLocations();// Initialize the StartCoordinate
            }

            
        }

        public MapResponseDto GetMap() { return Map; }

        public MapResponseDto AddMap(MapRequestDto map)
        {
            Map.StartCoordinate.Long = map.StartCoordinate.Long;
            Map.StartCoordinate.Lat = map.StartCoordinate.Lat;
            Map.StartCoordinate.OrderNumber = 1;
            Map.Zoomlevel = map.Zoomlevel;
            Map.Locations = _locationService.GetAllLocations();

            return Map;

           
        }

        public MapResponseDto PatchMap(MapRequestDto updatedMap)
        {

            if (updatedMap.Zoomlevel != 0.0f)
            {
                Map.Zoomlevel = updatedMap.Zoomlevel;
            }

            if (updatedMap.StartCoordinate != null)
            {
                if(updatedMap.StartCoordinate.Long != 0)
                {
                    // Check if the longitude has changed
                    if (Map.StartCoordinate.Long != updatedMap.StartCoordinate.Long)
                    {
                        Map.StartCoordinate.Long = updatedMap.StartCoordinate.Long;
                    }
                }
                
                if(updatedMap.StartCoordinate.Lat != 0)
                {
                    // Check if the latitude has changed
                if (Map.StartCoordinate.Lat != updatedMap.StartCoordinate.Lat)
                {
                    Map.StartCoordinate.Lat = updatedMap.StartCoordinate.Lat;
                }
                }
                
            }

            return Map;
        }



    }
}
