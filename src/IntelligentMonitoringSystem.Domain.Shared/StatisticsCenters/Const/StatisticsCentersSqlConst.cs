namespace IntelligentMonitoringSystem.Domain.Shared.StatisticsCenters.Const;

/// <summary>
///     统计中心相关sql语句
/// </summary>
public class StatisticsCentersSqlConst
{
    /// <summary>
    ///     统计访问趋势
    /// </summary>
    public const string TendencyStatisticsSql = """
                                                with currentTmp as (select IF(HOUR(access_time) < 10, HOUR(access_time), DATE_FORMAT(access_time, '%H')) AS Hour, access_type as AccessType
                                                                    from intelligent_monitoring_system.personnel_access_record
                                                                    where access_time between @currentStartOfDay and @currentEndOfDay)
                                                select Hour, AccessType, count(1) as Count
                                                from currentTmp
                                                group by Hour, AccessType;

                                                with dayOfMonthTmp as (select date(access_time) AS DayOfMonth, access_type as AccessType
                                                                      from intelligent_monitoring_system.personnel_access_record
                                                                      where access_time between @dayOfMonthStart and @currentEndOfDay)
                                                select DayOfMonth, accesstype, count(1) as Count
                                                from dayOfMonthTmp
                                                group by DayOfMonth, AccessType;
                                                """;
}