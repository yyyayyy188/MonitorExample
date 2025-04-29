namespace IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;

/// <summary>
///     领域事件基础对象
/// </summary>
public abstract class DomainEventBase : IDomainEvent
{
    /// <summary>
    ///     事件Id
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();

    /// <summary>
    ///     发生时间
    /// </summary>
    public DateTime OccurredOn { get; } = DateTime.Now;

    /// <summary>
    ///     事件流Id
    /// </summary>
    public abstract string StreamId { get; }
}