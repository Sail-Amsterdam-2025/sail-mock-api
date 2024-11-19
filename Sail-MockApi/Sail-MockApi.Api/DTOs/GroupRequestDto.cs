namespace Sail_MockApi.Api.DTOs
{
    public class GroupRequestDto
    {
        public string Name { get; set; }
        public string RoleId { get; set; }
        public GroupLeaderRequestDto? GroupLeader { get; set; }
    }
}
