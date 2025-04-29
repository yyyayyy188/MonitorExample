namespace IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;

/// <summary>
///     基础聚合根.
/// </summary>
public abstract class AggregateRoot : IAggregateRoot, IGeneratesDomainEvents
{
    /// <summary>
    ///     领域事件集合.
    /// </summary>
    private readonly List<DomainEventRecord> _events = [];

    #region 领域事件

    /// <summary>
    ///     获取事件.
    /// </summary>
    /// <returns>IReadOnlyCollection</returns>
    public virtual IReadOnlyCollection<DomainEventRecord> GetEvents()
    {
        return _events.AsReadOnly();
    }

    /// <summary>
    ///     清除领域事件
    /// </summary>
    public virtual void ClearEvents()
    {
        _events.Clear();
    }

    /// <summary>
    ///     添加领域事件
    /// </summary>
    /// <param name="domainEvent">Domain event.</param>
    public void AddEvent(DomainEventRecord domainEvent)
    {
        _events.Add(domainEvent);
    }

    /// <summary>
    ///     添加领域事件
    /// </summary>
    /// <param name="domainEvents">Domain event.</param>
    public void AddEvents(IReadOnlyCollection<DomainEventRecord> domainEvents)
    {
        _events.AddRange(domainEvents);
    }

    #endregion
}