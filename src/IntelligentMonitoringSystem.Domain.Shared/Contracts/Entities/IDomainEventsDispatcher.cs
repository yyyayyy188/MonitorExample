namespace IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;

/// <summary>
///     DomainEventsDispatcher
/// </summary>
public interface IDomainEventsDispatcher
{
    /// <summary>
    ///     Dispatch
    /// </summary>
    /// <param name="event">event</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>Task</returns>
    Task DispatchEventAsync<T>(T @event) where T : class, IDomainEvent;

    /// <summary>
    ///     Dispatch
    /// </summary>
    /// <param name="events">events</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>Task</returns>
    Task DispatchEventAsync<T>(List<T> events) where T : class, IDomainEvent;

    /// <summary>
    ///     DispatchImmediateEvents
    /// </summary>
    /// <param name="events">events</param>
    /// <typeparam name="T">T</typeparam>
    /// <returns>Task</returns>
    Task DispatchImmediateEvents<T>(List<T> events) where T : class, IDomainEvent;

    /// <summary>
    ///     DispatchAndClearEvents
    /// </summary>
    /// <param name="domainArray">domainArray</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>Task</returns>
    Task DispatchAndClearEvents<T>(IEnumerable<T> domainArray) where T : class, IGeneratesDomainEvents;
}