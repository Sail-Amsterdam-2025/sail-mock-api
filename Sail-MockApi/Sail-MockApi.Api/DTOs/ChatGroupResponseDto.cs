namespace Sail_MockApi.Api.DTOs;

public class ChatGroupResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<GroupUserResponseDto>? Users { get; set; }
    public List<ChatMessageResponseDto>? Messages { get; set; }
    
    public ChatGroupResponseDto(){}

    public ChatGroupResponseDto(ChatGroupRequestDto requestDto)
    {
        this.Id = Guid.NewGuid();
        this.Name = requestDto.Name;
        this.Users = requestDto.Users;
    }
    public ChatGroupResponseDto(string name, string id)
    {
        Id = Guid.Parse(id);
        Name = name;
        Users = new List<GroupUserResponseDto>
        {
            new GroupUserResponseDto("Henk", "Frans"),
            new GroupUserResponseDto("Jan", "Peter"),
            new GroupUserResponseDto("Donald", "'small loan' Trump"),
            new GroupUserResponseDto("Joe", "'not so bright' Biden"),
            new GroupUserResponseDto("Michael", "'HEHE' Jackson")
        };
    }
}