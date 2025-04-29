using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.Extensions;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Const;
using StackExchange.Redis;

namespace IntelligentMonitoringSystem.Infrastructure.Redis.PersonnelAccessRecords.Repositories;

/// <summary>
///     考勤记录延迟事件
/// </summary>
/// <param name="redisConnectionMultiplexer">redis连接.</param>
public class PersonnelAccessRecordDelayRepository(IConnectionMultiplexer redisConnectionMultiplexer)
    : IPersonnelAccessRecordDelayRepository
{
    /// <summary>
    ///     延迟事件
    /// </summary>
    /// <param name="eventIdentifier">事件标识.</param>
    /// <param name="eventTime">事件事件.</param>
    /// <returns>Task.</returns>
    public async Task PushDelayEventAsync(string eventIdentifier, DateTime eventTime)
    {
        var database = redisConnectionMultiplexer.GetDatabase();
        await database.SortedSetAddAsync(PersonnelAccessRecordConst.DelayEventKey, eventIdentifier, eventTime.ToUnixTimestamp());
    }

    /// <summary>
    ///     查询延迟事件
    /// </summary>
    /// <param name="deadline">截止时间</param>
    /// <returns>待处理的事件集合</returns>
    public async Task<List<Guid>> QueryDelayEventAsync(DateTime deadline)
    {
        var database = redisConnectionMultiplexer.GetDatabase();
        var members =
            await database.SortedSetRangeByScoreAsync(PersonnelAccessRecordConst.DelayEventKey, 0, deadline.ToUnixTimestamp());
        return members.Length == 0 ? [] : members.Select(x => Guid.Parse(x!)).ToList();
    }

    /// <summary>
    ///     清除延迟事件
    /// </summary>
    /// <param name="members">members</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task ClearDelayEventAsync(List<Guid>? members)
    {
        if (members == null || members.Count == 0)
        {
            return;
        }

        var database = redisConnectionMultiplexer.GetDatabase();
        await database.SortedSetRemoveAsync(
            PersonnelAccessRecordConst.DelayEventKey,
            members.Select(x => new RedisValue(x.ToString())).ToArray());
    }
}