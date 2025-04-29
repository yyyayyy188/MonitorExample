namespace IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;

/// <summary>
///     生成领域事件
/// </summary>
public interface IGeneratesDomainEvents
{
    /// <summary>
    ///     获取领域事件
    /// </summary>
    /// <returns>IReadOnlyCollection<DomainEventRecord /></returns>
    IReadOnlyCollection<DomainEventRecord> GetEvents();

    /// <summary>
    ///     清除事件
    /// </summary>
    void ClearEvents();
}