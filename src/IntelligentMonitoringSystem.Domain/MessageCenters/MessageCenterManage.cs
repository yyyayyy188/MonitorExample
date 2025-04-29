using System.Threading.Channels;

namespace IntelligentMonitoringSystem.Domain.MessageCenters;

/// <summary>
///     消息中心管理
/// </summary>
public class MessageCenterManage : IMessageCenterManage
{
    private readonly Channel<EventMessage> _queue;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MessageCenterManage" /> class.
    /// </summary>
    public MessageCenterManage()
    {
        var opts = new BoundedChannelOptions(2000) { FullMode = BoundedChannelFullMode.DropOldest };
        _queue = Channel.CreateBounded<EventMessage>(opts);
    }

    /// <summary>
    ///     推送
    /// </summary>
    /// <param name="eventMessage">eventMessage</param>
    /// <returns>ValueTask</returns>
    public async ValueTask PushAsync(EventMessage eventMessage)
    {
        await _queue.Writer.WriteAsync(eventMessage);
    }

    /// <summary>
    ///     获取队列
    /// </summary>
    /// <returns>EventMessage</returns>
    public async ValueTask<EventMessage> PullAsync()
    {
        var result = await _queue.Reader.ReadAsync();
        return result;
    }
}