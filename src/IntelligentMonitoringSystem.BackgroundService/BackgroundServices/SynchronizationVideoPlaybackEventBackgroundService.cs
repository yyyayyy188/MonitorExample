// <copyright file="SynchronizationVideoPlaybackEventBackgroundService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using IntelligentMonitoringSystem.BackgroundService.BackgroundServices.Queue;
using IntelligentMonitoringSystem.Domain.EventSources;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using MediatR;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.BackgroundService.BackgroundServices;

/// <summary>
///     视频回放事件同步服务
/// </summary>
public class SynchronizationVideoPlaybackEventBackgroundService(
    IBlockQueue blockQueue,
    ILogger<SynchronizationVideoPlaybackEventBackgroundService> logger,
    IServiceScopeFactory serviceScopeFactory)
    : Microsoft.Extensions.Hosting.BackgroundService
{
    /// <summary>
    ///     启动
    /// </summary>
    /// <param name="stoppingToken">stoppingToken</param>
    /// <returns>Task</returns>
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var eventStream = await blockQueue.PullAsync();
                if (eventStream == null)
                {
                    continue;
                }

                using var scope = serviceScopeFactory.CreateScope();
                var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
                var basicRepository = scope.ServiceProvider.GetRequiredService<IBasicRepository<EventStream>>();

                if (string.IsNullOrWhiteSpace(eventStream.FullTypeName))
                {
                    await UpdateEventStreamAsync(eventStream.Id, false, "事件类型为空", basicRepository);
                }

                var eventType = Type.GetType(eventStream.FullTypeName!, false);
                if (eventType == null)
                {
                    await UpdateEventStreamAsync(eventStream.Id, false, "事件类型无法解析", basicRepository);
                }

                try
                {
                    var @event = JsonConvert.DeserializeObject(eventStream.Data, eventType!);
                    if (@event == null)
                    {
                        await UpdateEventStreamAsync(eventStream.Id, false, "事件类型数据为空", basicRepository);
                    }
                    else
                    {
                        await publisher.Publish(@event, stoppingToken);
                        await UpdateEventStreamAsync(eventStream.Id, true, null, basicRepository);
                    }
                }
                catch (Exception ex)
                {
                    await UpdateEventStreamAsync(
                        eventStream.Id,
                        false,
                        TruncateString($"事件类型处理失败:{ex.InnerException?.Message ?? ex.Message}"),
                        basicRepository);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failure while processing queue {Ex}", ex.InnerException?.Message ?? ex.Message);
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }

    /// <summary>
    ///     截取字符串
    /// </summary>
    /// <param name="input">待处理字符串</param>
    /// <param name="maxLength">最大长度</param>
    /// <returns>处理后的字符串</returns>
    private static string? TruncateString(string? input, int maxLength = 512)
    {
        if (input == null)
        {
            return null;
        }

        return input.Length > maxLength ? input.Substring(0, maxLength) : input;
    }

    /// <summary>
    ///     更新事件流状态
    /// </summary>
    /// <param name="id">事件流Id</param>
    /// <param name="isSuccess">是否成功</param>
    /// <param name="message">消息</param>
    /// <param name="basicRepository">基础对象</param>
    private async static Task UpdateEventStreamAsync(
        int id, bool isSuccess, string? message, [NotNull] IBasicRepository<EventStream> basicRepository)
    {
        if (isSuccess)
        {
            await basicRepository.ExecuteUpdateAsync(x => x.Id == id, setters => setters
                    .SetProperty(b => b.ProcessCount, b => b.ProcessCount + 1)
                    .SetProperty(b => b.Status, 2)
                    .SetProperty(b => b.LastProcessTime, DateTime.Now)
                    .SetProperty(b => b.Message, "成功"))
                ;
        }
        else
        {
            await basicRepository.ExecuteUpdateAsync(x => x.Id == id, setters => setters
                .SetProperty(b => b.ProcessCount, b => b.ProcessCount + 1)
                .SetProperty(b => b.Status, 3)
                .SetProperty(b => b.LastProcessTime, DateTime.Now)
                .SetProperty(b => b.Message, message));
        }
    }
}