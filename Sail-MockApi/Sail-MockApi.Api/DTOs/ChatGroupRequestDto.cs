namespace Sail_MockApi.Api.DTOs;

public class ChatGroupRequestDto
{
    public string Name { get; set; }
    public List<GroupUserResponseDto>? Users { get; set; }
}