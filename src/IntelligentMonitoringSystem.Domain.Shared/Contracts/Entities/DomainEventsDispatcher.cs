using IntelligentMonitoringSystem.Domain.Shared.EventSources.Events;
using MediatR;

namespace IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;

/// <summary>
///     Domain Events Dispatcher
/// </summary>
/// <param name="publisher">publisher</param>
public class DomainEventsDispatcher(
    IPublisher publisher)
    : IDomainEventsDispatcher
{
    /// <summary>
    ///     Dispatch
    /// </summary>
    /// <param name="event">event</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>Task</returns>
    public async Task DispatchEventAsync<T>(T @event) where T : class, IDomainEvent
    {
        await publisher.Publish(new StoreEventSourceEvent(new List<IDomainEvent> { @event }));
    }

    /// <summary>
    ///     Dispatch
    /// </summary>
    /// <param name="events">event</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>Task</returns>
    public async Task DispatchEventAsync<T>(List<T> events) where T : class, IDomainEvent
    {
        if (events.Count == 0)
        {
            return;
        }

        await publisher.Publish(new StoreEventSourceEvent(events));
    }

    /// <summary>
    ///     DispatchImmediateEvents
    /// </summary>
    /// <param name="events">events</param>
    /// <typeparam name="T">T</typeparam>
    /// <returns>Task</returns>
    public async Task DispatchImmediateEvents<T>(List<T> events) where T : class, IDomainEvent
    {
        if (events.Count == 0)
        {
            return;
        }

        foreach (var domainEvent in events)
        {
            await publisher.Publish(domainEvent);
        }
    }

    /// <summary>
    ///     Dispatch and clear events
    /// </summary>
    /// <param name="domainArray">domainArray</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task DispatchAndClearEvents<T>(IEnumerable<T> domainArray) where T : class, IGeneratesDomainEvents
    {
        foreach (var domain in domainArray)
        {
            var domainEvents = domain.GetEvents().ToArray();
            if (domainEvents.Length == 0)
            {
                continue;
            }

            domain.ClearEvents();
            await publisher.Publish(
                new StoreEventSourceEvent(domainEvents.OrderByDescending(x => x.EventOrder).Select(x => x.EventData)));
        }
    }
}