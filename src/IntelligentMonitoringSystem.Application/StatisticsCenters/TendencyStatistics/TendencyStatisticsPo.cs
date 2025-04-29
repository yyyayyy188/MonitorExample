using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;

namespace IntelligentMonitoringSystem.Application.StatisticsCenters.TendencyStatistics;

/// <summary>
///     趋势统计查询
/// </summary>
public class TendencyStatisticsPo
{
    /// <summary>
    ///     小时
    /// </summary>
    public int Hour { get; set; }

    /// <summary>
    ///     日期
    /// </summary>
    public DateTime DayOfMonth { get; set; }

    /// <summary>
    ///     访问类型
    /// </summary>
    public AccessTypeSmartEnum AccessType { get; set; }

    /// <summary>
    ///     访问次数
    /// </summary>
    public int Count { get; set; }
}