namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;

/// <summary>
///     延迟事件
/// </summary>
public interface IPersonnelAccessRecordDelayRepository
{
    /// <summary>
    ///     延迟事件
    /// </summary>
    /// <param name="eventIdentifier">事件标识</param>
    /// <param name="eventTime">事件事件</param>
    /// <returns>Task</returns>
    Task PushDelayEventAsync(string eventIdentifier, DateTime eventTime);
}