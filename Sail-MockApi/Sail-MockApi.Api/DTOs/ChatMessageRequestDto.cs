using System.Buffers.Text;

namespace Sail_MockApi.Api.DTOs;

public class ChatMessageRequestDto
{
    public Guid UserId { get; set; }
    public Guid ChatGroupId { get; set; }
    public string? Message { get; set; }
    //. TODO: Make the image prop the correct type
    public string? Image { get; set; }

    public bool ContainsContent()
    {
        return Message is not null || Image is not null;
    }
}