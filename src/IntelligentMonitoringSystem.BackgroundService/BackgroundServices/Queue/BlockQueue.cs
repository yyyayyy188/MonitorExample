// <copyright file="BlockQueue.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Threading.Channels;
using IntelligentMonitoringSystem.Domain.EventSources;

namespace IntelligentMonitoringSystem.BackgroundService.BackgroundServices.Queue;

/// <summary>
///     阻塞队列
/// </summary>
public class BlockQueue : IBlockQueue
{
    private readonly Channel<EventStream> queue;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BlockQueue" /> class.
    /// </summary>
    public BlockQueue()
    {
        var opts = new BoundedChannelOptions(2000) { FullMode = BoundedChannelFullMode.DropOldest };
        queue = Channel.CreateBounded<EventStream>(opts);
    }

    /// <summary>
    ///     推送
    /// </summary>
    /// <param name="eventStream">eventStream</param>
    /// <returns>ValueTask</returns>
    public async ValueTask PushAsync(EventStream? eventStream)
    {
        if (eventStream == null)
        {
            return;
        }

        await queue.Writer.WriteAsync(eventStream);
    }

    /// <summary>
    ///     获取队列
    /// </summary>
    /// <returns>EventStream</returns>
    public async ValueTask<EventStream?> PullAsync()
    {
        var result = await queue.Reader.ReadAsync();
        return result;
    }
}