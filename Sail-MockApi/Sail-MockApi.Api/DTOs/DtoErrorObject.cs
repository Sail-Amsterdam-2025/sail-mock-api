using Sail_MockApi.Api.Helpers;

namespace Sail_MockApi.Api.DTOs;
/// <summary>
/// An object of this type is returned when the data provided to an endpoint does not match the correct format
/// </summary>
public class DtoErrorObject<T>
{
    private const string ErrorMessage = "The provided body does not match the expected data format. Please use the correct format as provided in this message.";
    public string Message { get; set; } = ErrorMessage;
    public object Format { get; set; } = DtoFormatGenerator.CreateFormat(typeof(T));
}