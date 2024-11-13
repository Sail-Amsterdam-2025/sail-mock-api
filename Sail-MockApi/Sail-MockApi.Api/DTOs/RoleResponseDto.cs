namespace Sail_MockApi.Api.DTOs
{
    public class RoleResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public RoleResponseDto(RoleRequestDto roleRequest, string id) {
        
            Id = id;
            Name = roleRequest.Name;
        }

        public RoleResponseDto(string name, string id)
        {

            Id = id;
            Name = name;
        }
    }
}
