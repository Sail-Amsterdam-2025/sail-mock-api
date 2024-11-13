namespace Sail_MockApi.Api.DTOs;

public class TaskResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid? GroupId { get; set; }
    public Guid? RoleId { get; set; }
    public TaskResponseDto() { }

    public TaskResponseDto(string id, string name, string description, string? groupId = null, string? roleId = null)
    {
        Id = Guid.Parse(id);
        Name = name;
        Description = description;
        GroupId = groupId != null ? Guid.Parse(groupId) : null;
        RoleId = roleId != null ? Guid.Parse(roleId) : null;
    }

    public TaskResponseDto(TaskRequestDto request)
    {
        Id = Guid.NewGuid();
        Name = request.Name;
        Description = request.Description;
        GroupId = request.GroupId != null ? Guid.Parse(request.GroupId) : null;
        RoleId = request.RoleId != null ? Guid.Parse(request.RoleId) : null;
    }
}