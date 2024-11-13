namespace Sail_MockApi.Api.RequestQueries;

public class GetChatGroupsQuery
{
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public int? MessageLimit { get; set; }
    public int? MessageOffset { get; set; }
    public string? Name { get; set; }
    public string? UserId { get; set; }
    public bool? IncludeMessages { get; set; }
    public string? GetMessagesBefore { get; set; }
    public string? GetMessagesAfter { get; set; }
}