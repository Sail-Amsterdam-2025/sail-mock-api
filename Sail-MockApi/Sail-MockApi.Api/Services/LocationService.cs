using Sail_MockApi.Api.DTOs;
using System.Data;

namespace Sail_MockApi.Api.Services
{
    public class LocationService
    {
        private static List<LocationResponseDto> Locations { get; set; } = new List<LocationResponseDto>();

        public LocationService()
        {
            AddDataToList();

        }
        public void AddDataToList()
        {
            if (Locations.Count == 0)
            {
                Locations.Add(new LocationResponseDto(Guid.NewGuid().ToString(), "ehbo post 1", "PinPoint", "Green", new List<CoordinateResponseDto> { new CoordinateResponseDto(Guid.NewGuid().ToString(), 1, 4.891140027255756, 52.40472178878887) }));
                Locations.Add(new LocationResponseDto(Guid.NewGuid().ToString(), "Area 1", "Area", "Purple", new List<CoordinateResponseDto> { new CoordinateResponseDto(Guid.NewGuid().ToString(), 1, 4.893310518382505, 52.402979421681266),
                new CoordinateResponseDto(Guid.NewGuid().ToString(), 2, 4.889426481683392, 52.41245706779914), new CoordinateResponseDto(Guid.NewGuid().ToString(), 3, 4.8985653915636584, 52.406673158281656)}));
            }

        }

            public List<LocationResponseDto> GetAllLocations(int limit = 50, int offset = 0, string name = null)
        {
            var query = Locations.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(l => l.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }
            query = query.Skip(offset).Take(limit);

            return query.ToList();
        }

        public LocationResponseDto GetLocationById(string id)
        {
            return Locations.FirstOrDefault(l => l.Id == id);
        }

        public LocationResponseDto AddLocation(LocationRequestDto locationRequest)
        {
            string locationId = Guid.NewGuid().ToString();

            var newLocation = new LocationResponseDto
            {
                Id = locationId,
                Name = locationRequest.Name,
                Coordinates = new List<CoordinateResponseDto>()
            };

            
            foreach (var coordRequest in locationRequest.Coordinates)
            {
                var newCoordinate = new CoordinateResponseDto
                {
                    Id = Guid.NewGuid().ToString(),            
                    OrderNumber = coordRequest.OrderNumber,
                    Long = coordRequest.Long,
                    Lat = coordRequest.Lat
                };
                newLocation.Coordinates.Add(newCoordinate);
            }

            Locations.Add(newLocation);
            return newLocation;
        }

        public LocationResponseDto PatchLocationById(string locationId, LocationRequestDto updatedLocation)
        {

            var location = Locations.FirstOrDefault(l => l.Id == locationId);

            if (location == null)
            {
                return null; 
            }

            if (!string.IsNullOrEmpty(updatedLocation.Name))
            {
                location.Name = updatedLocation.Name;
            }


            if (updatedLocation.Coordinates != null && updatedLocation.Coordinates.Any())
            {
                foreach (var updatedCoord in updatedLocation.Coordinates)
                {

                    var existingCoord = location.Coordinates.FirstOrDefault(c => c.OrderNumber == updatedCoord.OrderNumber);

                    if (existingCoord != null)
                    {

                        existingCoord.Long = updatedCoord.Long;
                        existingCoord.Lat = updatedCoord.Lat;
                    }
                    else
                    {

                        var newCoordinate = new CoordinateResponseDto
                        {
                            Id = Guid.NewGuid().ToString(),
                            OrderNumber = updatedCoord.OrderNumber,
                            Long = updatedCoord.Long,
                            Lat = updatedCoord.Lat
                        };
                        location.Coordinates.Add(newCoordinate);
                    }
                }
            }
            return location;
        }

        public bool DeleteLocationById(string locationId)
        {
            var locationToDelete = Locations.FirstOrDefault(l => l.Id == locationId);

            if (locationToDelete != null)
            {
                Locations.Remove(locationToDelete);
                return true;
            }

            return false;
        }

    }
}
