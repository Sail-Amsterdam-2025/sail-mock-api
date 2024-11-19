namespace Sail_MockApi.Api.DTOs;

public class GroupLeaderResponseDto
{
    public Guid Id { get; set; }
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
    public string PhoneNumber { get; set; }

    public GroupLeaderResponseDto(GroupLeaderRequestDto groupLeaderRequestDto)
    {
        Id = Guid.NewGuid();
        if (!Guid.TryParse(groupLeaderRequestDto.GroupId, out var groupId))
        {
            throw new ArgumentException("Invalid group ID (not the correct GUID format)");
        }
        GroupId = groupId;
        if (!Guid.TryParse(groupLeaderRequestDto.UserId, out var userId))
        {
            throw new ArgumentException("Invalid user ID (not the correct GUID format)");
        }

        UserId = userId;
        PhoneNumber = groupLeaderRequestDto.PhoneNumber;
    }

    public GroupLeaderResponseDto(Guid groupId, Guid userId, string phoneNumber)
    {
        Id = Guid.NewGuid();
        GroupId = groupId;
        UserId = userId;
        PhoneNumber = phoneNumber;
    }
}