namespace Sail_MockApi.Api.DTOs;

public class GroupUserResponseDto(string firstName, string lastName)
{
    public Guid UserId { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;

}