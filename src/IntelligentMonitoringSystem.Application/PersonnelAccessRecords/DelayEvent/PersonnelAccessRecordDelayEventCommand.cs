using IntelligentMonitoringSystem.Application.Common.Contracts;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.DelayEvent;

/// <summary>
///     考勤记录超时事件
/// </summary>
public class PersonnelAccessRecordDelayEventCommand : CommandBase
{
    /// <summary>
    ///     事件标识
    /// </summary>
    public Guid EventIdentifier { get; set; }
}