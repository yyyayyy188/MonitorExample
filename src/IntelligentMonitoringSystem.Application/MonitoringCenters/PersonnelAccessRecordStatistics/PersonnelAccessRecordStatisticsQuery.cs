using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.MonitoringCenters.Dtos;

namespace IntelligentMonitoringSystem.Application.MonitoringCenters.PersonnelAccessRecordStatistics;

/// <summary>
///     统计查询
/// </summary>
public class PersonnelAccessRecordStatisticsQuery(DateTime filterDateTime) : QueryBase<PersonnelAccessRecordStatisticsDto>
{
    /// <summary>
    ///     过滤时间
    /// </summary>
    public DateTime FilterDateTime { get; set; } = filterDateTime;
}