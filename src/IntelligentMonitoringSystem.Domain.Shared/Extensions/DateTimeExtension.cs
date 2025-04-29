namespace IntelligentMonitoringSystem.Domain.Shared.Extensions;

/// <summary>
///     时间扩展类
/// </summary>
public static class DateTimeExtension
{
    private readonly static DateTime UnixTimeStart = DateTime.UnixEpoch;

    /// <summary>
    ///     时间戳转本时区日期时间
    /// </summary>
    /// <param name="timeStamp">timeStamp</param>
    /// <returns>DateTime</returns>
    public static DateTime ToDateTime(this long timeStamp)
    {
        return UnixTimeStart.AddMilliseconds(timeStamp).ToLocalTime();
    }

    /// <summary>
    ///     ToUnixTimestamp
    /// </summary>
    /// <param name="dateTime">dateTime</param>
    /// <returns>long</returns>
    public static long ToUnixTimestamp(this DateTime dateTime)
    {
        var dateTimeOffset = new DateTimeOffset(dateTime);
        return dateTimeOffset.ToUnixTimeMilliseconds();
    }
}