using Humanizer.Localisation.TimeToClockNotation;

namespace Sail_MockApi.Api.DTOs
{
    public class GroupResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string RoleId { get; set; }
        public string TeamLeaderId { get; set; }



        public GroupResponseDto(GroupRequestDto groupRequest, string id) {
            Id = id;
            Name = groupRequest.Name;
            RoleId = groupRequest.RoleId;
            TeamLeaderId = groupRequest.TeamLeaderId;

        }

        public GroupResponseDto(string id, string name, string roleId, string teamLeaderId)
        {
            Id = id;
            Name = name;
            RoleId = roleId;
            TeamLeaderId = teamLeaderId;
        }
    }
}
