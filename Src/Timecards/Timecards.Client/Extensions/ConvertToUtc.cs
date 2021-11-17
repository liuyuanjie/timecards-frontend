using System;

public static class DateTimeExtensions
{
    /// <summary>
    /// Convert the local or unspecific date time to utc.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>A UTC Date Time</returns>
    public static DateTime ConvertToUTCDate(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, DateTimeKind.Utc);
    }
}