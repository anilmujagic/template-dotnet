namespace MyApp.Common.Extensions;

public static class DateTimeExtensions
{
    public static DateTime EndOfDay(this DateTime dateTime) => dateTime.Date.AddDays(1).AddTicks(-1);

    public static string IsoDateString(this DateTime dateTime) => dateTime.ToString("yyyy-MM-dd");

    public static DateTime SetKind(this DateTime dateTime, DateTimeKind kind) =>
        DateTime.SpecifyKind(dateTime, kind);

    public static long ToUnixTimeSeconds(this DateTime dateTime) =>
        new DateTimeOffset(dateTime).ToUnixTimeSeconds();

    public static long ToUnixTimeMilliseconds(this DateTime dateTime) =>
        new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
}
