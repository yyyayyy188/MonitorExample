using IntelligentMonitoringSystem.Domain.EventSources;

namespace IntelligentMonitoringSystem.BackgroundService.BackgroundServices.Queue;

/// <summary>
///     阻塞队列
/// </summary>
public interface IBlockQueue
{
    /// <summary>
    ///     获取事件
    /// </summary>
    /// <returns>
    ///     ValueTask<EventStream />
    /// </returns>
    ValueTask<EventStream?> PullAsync();

    /// <summary>
    ///     推送事件
    /// </summary>
    /// <param name="eventStream">eventStream</param>
    /// <returns>ValueTask</returns>
    ValueTask PushAsync(EventStream? eventStream);
}