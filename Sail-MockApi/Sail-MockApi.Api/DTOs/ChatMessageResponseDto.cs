using Sail_MockApi.Api.Helpers;

namespace Sail_MockApi.Api.DTOs;

public class ChatMessageResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; } 
    public Guid ChatGroupId { get; set; } 
    public DateTime SentAt { get; set; }
    public string? Message { get; set; }
    //. TODO: Make the image prop the correct type
    public string? Image { get; set; }

    public ChatMessageResponseDto(ChatMessageRequestDto chatMessageRequestDto)
    {
        this.Id = Guid.NewGuid();
        this.UserId = chatMessageRequestDto.UserId;
        this.ChatGroupId = chatMessageRequestDto.ChatGroupId;
        this.SentAt = DateHelper.GetCurrentDateTimeUtc();
        this.Message = chatMessageRequestDto.Message;
        this.Image = chatMessageRequestDto.Image;
    }

    public ChatMessageResponseDto(string id, string userId, string chatGroupId, string? message = null, string? image = null)
    {
        this.Id = Guid.Parse(id);
        this.UserId = Guid.Parse(userId);
        this.ChatGroupId = Guid.Parse(chatGroupId);
        this.SentAt = DateHelper.GetCurrentDateTimeUtc();
        this.Message = message;
        this.Image = image;
    }
}