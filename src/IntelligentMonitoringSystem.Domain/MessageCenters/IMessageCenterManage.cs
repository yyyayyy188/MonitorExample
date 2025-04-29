namespace IntelligentMonitoringSystem.Domain.MessageCenters;

/// <summary>
///     消息中心
/// </summary>
public interface IMessageCenterManage
{
    /// <summary>
    ///     获取事件
    /// </summary>
    /// <returns>
    ///     ValueTask<EventMessage />
    /// </returns>
    ValueTask<EventMessage> PullAsync();

    /// <summary>
    ///     推送事件
    /// </summary>
    /// <param name="eventMessage">事件消息</param>
    /// <returns>ValueTask</returns>
    ValueTask PushAsync(EventMessage eventMessage);
}