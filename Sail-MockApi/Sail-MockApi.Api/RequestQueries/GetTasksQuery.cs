namespace Sail_MockApi.Api.RequestQueries;

public class GetTasksQuery
{
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public string? Name { get; set; }
    public string? GroupId { get; set; }
    public string? RoleId { get; set; }
    
    
}