using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using MediatR;

namespace IntelligentMonitoringSystem.Domain.Shared.EventSources.Events;

/// <summary>
///     存储事件源事件
/// </summary>
/// <param name="events">events</param>
public class StoreEventSourceEvent(IEnumerable<IDomainEvent> events) : INotification
{
    /// <summary>
    ///     事件
    /// </summary>
    public IEnumerable<IDomainEvent> Events { get; set; } = events;
}