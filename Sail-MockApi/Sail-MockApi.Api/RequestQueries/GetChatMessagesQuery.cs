namespace Sail_MockApi.Api.RequestQueries;

public class GetChatMessagesQuery
{
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public string? ChatGroupId { get; set; }
    public int? UserId { get; set; }
    public string? GetMessagesBefore { get; set; }
    public string? GetMessagesAfter { get; set; }
}