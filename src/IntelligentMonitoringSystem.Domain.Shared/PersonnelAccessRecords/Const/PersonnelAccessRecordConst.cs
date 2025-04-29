namespace IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Const;

/// <summary>
///     考勤记录常量
/// </summary>
public static class PersonnelAccessRecordConst
{
    /// <summary>
    ///     考勤记录预警时间格式
    /// </summary>
    public const string FormatWaringDateTime = "yyyy-MM-dd HH:mm:00";

    /// <summary>
    ///     考勤记录查询时间格式
    /// </summary>
    public const string FormatFilterDateTime = "yyyy-MM-dd HH:mm:ss";

    /// <summary>
    ///     延迟事件标识
    /// </summary>
    public const string DelayEventKey = "PersonnelAccessRecord:DelayEvent";

    /// <summary>
    ///     Topic Name
    /// </summary>
    public const string TopicName = "topic_personnel_access_record_delay";

    /// <summary>
    ///     Topic Name
    /// </summary>
    public const string SubscriptionName = "IntelligentMonitoringSystem_DelaySubscription";
}