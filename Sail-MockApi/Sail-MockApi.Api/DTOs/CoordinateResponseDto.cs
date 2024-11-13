using Microsoft.Build.ObjectModelRemoting;

namespace Sail_MockApi.Api.DTOs
{
    public class CoordinateResponseDto
    {
        public string Id { get; set; }
        public int OrderNumber { get; set; }
        public double Lat {  get; set; }
        public double Long { get; set; }

        public CoordinateResponseDto(CoordinateRequestDto coordinateRequest, string id) {
        
            Id = id;
            OrderNumber = coordinateRequest.OrderNumber;
            Lat = coordinateRequest.Lat;
            Long = coordinateRequest.Long;
        }

        public CoordinateResponseDto(string id, int orderNumber, double lat, double @long)
        {
            Id = id;
            OrderNumber = orderNumber;
            Lat = lat;
            Long = @long;
        }

        public CoordinateResponseDto() { }
    }
}
