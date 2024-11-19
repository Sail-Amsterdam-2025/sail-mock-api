using Humanizer.Localisation.TimeToClockNotation;

namespace Sail_MockApi.Api.DTOs
{
    public class GroupResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string RoleId { get; set; }
        public GroupLeaderResponseDto? GroupLeader { get; set; }



        public GroupResponseDto(GroupRequestDto groupRequest, string id) {
            Id = id;
            Name = groupRequest.Name;
            RoleId = groupRequest.RoleId;
            if (groupRequest.GroupLeader != null)
            {
                GroupLeader = new GroupLeaderResponseDto(groupRequest.GroupLeader);
            }

        }

        public GroupResponseDto(string id, string name, string roleId, GroupLeaderResponseDto groupLeaderResponseDto)
        {
            Id = id;
            Name = name;
            RoleId = roleId;
            GroupLeader = groupLeaderResponseDto;
        }
    }
}
