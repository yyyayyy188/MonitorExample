using MediatR;

namespace IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;

/// <summary>
///     The base interface for all domain events.
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    ///     The unique identifier of the event.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    ///     The date and time the event occurred.
    /// </summary>
    DateTime OccurredOn { get; }

    /// <summary>
    ///     事件流Id
    /// </summary>
    string StreamId { get; }
}