namespace Baseplate.Helpers;

public static class DateTimeHelpers
{
    //TODO Hardcode for time being
    private static readonly TimeZoneInfo AestTimeZone = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");

    public static DateTime ConvertToAest(this DateTime dateTime)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(dateTime, AestTimeZone);
    }
}