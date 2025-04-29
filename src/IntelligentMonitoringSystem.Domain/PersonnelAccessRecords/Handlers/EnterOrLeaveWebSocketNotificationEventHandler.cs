using IntelligentMonitoringSystem.Domain.MessageCenters;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Events;
using IntelligentMonitoringSystem.Domain.Shared.MessageCenters.Enums;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using MediatR;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Handlers;

/// <summary>
///     进入或离开WebSocket通知事件处理器
/// </summary>
public class EnterOrLeaveWebSocketNotificationEventHandler(IMessageCenterManage messageCenterManage)
    : INotificationHandler<WebSocketNotificationEvent>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="notification">notification</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Task</returns>
    public async Task Handle(WebSocketNotificationEvent notification, CancellationToken cancellationToken)
    {
        await messageCenterManage.PushAsync(new EventMessage
        {
            EventSource =
                notification.AccessType == AccessTypeSmartEnum.Leave ? EventSourceSmartEnum.Leave : EventSourceSmartEnum.Enter,
            EventSourceData = new Dictionary<string, object> { ["EventIdentifier"] = notification.EventIdentifier }
        });
    }
}