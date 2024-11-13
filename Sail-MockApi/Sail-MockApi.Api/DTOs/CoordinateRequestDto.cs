using Humanizer.Localisation.TimeToClockNotation;

namespace Sail_MockApi.Api.DTOs
{
    public class CoordinateRequestDto
    {
        public int OrderNumber { get; set; }
        public double Lat {  get; set; }
        public double Long { get; set; }
    }
}
