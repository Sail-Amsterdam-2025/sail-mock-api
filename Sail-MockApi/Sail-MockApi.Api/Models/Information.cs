namespace Sail_MockApi.Api.Models
{
    public class Information
    {
        public Guid id { get; set; }
        public InfoCategory Category { get; set; }
        public string Title {  get; set; }
        public string Value { get; set; }

    }
}
