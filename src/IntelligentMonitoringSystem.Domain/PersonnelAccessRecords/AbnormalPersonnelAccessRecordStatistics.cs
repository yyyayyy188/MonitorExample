using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;

/// <summary>
///     异常外出统计信息
/// </summary>
public class AbnormalPersonnelAccessRecordStatistics : IEntity
{
    /// <summary>
    ///     月统计数量
    /// </summary>
    public int MonthCount { get; set; }

    /// <summary>
    ///     总统计数量
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    ///     季度统计数量
    /// </summary>
    public int QuarterCount { get; set; }

    /// <summary>
    ///     年统计数量
    /// </summary>
    public int YearCount { get; set; }
}