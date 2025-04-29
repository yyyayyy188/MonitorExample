using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.EventSources.Events;
using MediatR;

namespace IntelligentMonitoringSystem.Domain.EventSources.Handlers;

/// <summary>
///     领域事件处理器
/// </summary>
/// <param name="basicRepository">basicRepository</param>
public class StoreEventSourceEventHandler(IBasicRepository<EventStream> basicRepository)
    : INotificationHandler<StoreEventSourceEvent>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="notification">notification</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Task</returns>
    public async Task Handle(StoreEventSourceEvent notification, CancellationToken cancellationToken)
    {
        await basicRepository.BulkInsertAsync(notification.Events.Select(x => new EventStream(x.StreamId, x)), cancellationToken);
    }
}