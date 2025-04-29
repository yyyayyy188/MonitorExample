using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Events;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;
using MediatR;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Handlers;

/// <summary>
///     预计返回延迟事件
/// </summary>
public class ExpectedReturnTimeDelayEventHandler(IPersonnelAccessRecordDelayRepository personnelAccessRecordDelayRepository)
    : INotificationHandler<ExpectedReturnTimeDelayEvent>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="notification">notification</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Task</returns>
    public async Task Handle(ExpectedReturnTimeDelayEvent notification, CancellationToken cancellationToken)
    {
        await personnelAccessRecordDelayRepository.PushDelayEventAsync(
            notification.EventIdentifier.ToString(),
            notification.ExpectedReturnTime);
    }
}