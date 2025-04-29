using IntelligentMonitoringSystem.Application.Common.Contracts;

namespace IntelligentMonitoringSystem.Application.MonitoringCenters.QueryMonthAbnormalPersonnelAccessRecordStatistics;

/// <summary>
///     月度异常人员出入记录统计查询
/// </summary>
/// <param name="filterMonthTime">filterMonthTime</param>
public class QueryMonthAbnormalPersonnelAccessRecordStatisticsQuery(DateTime filterMonthTime) : QueryBase<List<int>>
{
    /// <summary>
    ///     过滤时间
    /// </summary>
    public DateTime FilterMonthTime { get; set; } = filterMonthTime;
}