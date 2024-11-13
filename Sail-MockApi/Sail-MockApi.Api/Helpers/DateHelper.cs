namespace Sail_MockApi.Api.Helpers;

public static class DateHelper
{
    /// <summary>
    /// This function returns the current datetime in Utc format (this is required for MongoDb and Azure Table Storage)
    /// </summary>
    /// <returns></returns>
    public static DateTime GetCurrentDateTimeUtc()
    {
        return DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
    }
}