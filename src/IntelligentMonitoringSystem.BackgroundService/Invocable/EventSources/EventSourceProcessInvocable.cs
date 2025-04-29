// <copyright file="EventSourceProcessInvocable.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Coravel.Invocable;
using IntelligentMonitoringSystem.BackgroundService.BackgroundServices.Queue;
using IntelligentMonitoringSystem.Domain.EventSources;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.EventSources.Const;

namespace IntelligentMonitoringSystem.BackgroundService.Invocable.EventSources;

/// <summary>
///     事件源处理
/// </summary>
/// <param name="readOnlyBasicRepository">readOnlyBasicRepository</param>
/// <param name="blockQueue">blockQueue</param>
public class EventSourceProcessInvocable(
    IReadOnlyBasicRepository<EventStream> readOnlyBasicRepository,
    IBlockQueue blockQueue)
    : IInvocable
{
    /// <summary>
    ///     执行
    /// </summary>
    /// <returns>Task</returns>
    public async Task Invoke()
    {
        var eventStreams =
            await readOnlyBasicRepository.QueryAsync(
                EventSourceSqlConst.QueryPendingRecordByTypeSql,
                new { count = 5, processCount = 3, type = "SynchronizationVideoPlaybackEvent" });
        var enumerable = eventStreams as EventStream[] ?? eventStreams.ToArray();
        if (enumerable.Length == 0)
        {
            return;
        }

        foreach (var item in enumerable)
        {
            await blockQueue.PushAsync(item);
        }
    }
}