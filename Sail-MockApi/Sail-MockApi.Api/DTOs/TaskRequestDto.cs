namespace Sail_MockApi.Api.DTOs;

public class TaskRequestDto(string name, string description, string? groupId = null, string? roleId = null)
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public string? GroupId { get; set; } = groupId;
    public string? RoleId { get; set; } = roleId;
}