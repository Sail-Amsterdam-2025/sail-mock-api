namespace Sail_MockApi.Api.DTOs;

public class DtoResponseObject<T>(string message, T responseObject)
{
    public string Message { get; set; } = message;
    public T ResponseObject { get; set; } = responseObject;
    
}