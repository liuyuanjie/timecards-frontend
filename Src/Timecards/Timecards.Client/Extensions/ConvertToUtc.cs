using System;

public static class DateTimeExtensions
{
    public static DateTime ConvertToUTCDate(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, DateTimeKind.Utc);
    }
}