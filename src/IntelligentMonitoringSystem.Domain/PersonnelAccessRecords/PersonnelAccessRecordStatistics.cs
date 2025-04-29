using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;

/// <summary>
///     出入统计信息
/// </summary>
public class PersonnelAccessRecordStatistics : IEntity
{
    /// <summary>
    ///     总数量
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    ///     进入数量
    /// </summary>
    public int EnterCount { get; set; }

    /// <summary>
    ///     离开数量
    /// </summary>
    public int LeaveCount { get; set; }

    /// <summary>
    ///     异常离开数量
    /// </summary>
    public int AbnormalLeaveCount { get; set; }
}