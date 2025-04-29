using Coravel.Invocable;
using IntelligentMonitoringSystem.Domain.EventSources;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.EventSources.Const;
using MediatR;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.WebApi.Invocable.EventSources;

/// <summary>
///     事件源处理
/// </summary>
/// <param name="readOnlyBasicRepository">readOnlyBasicRepository</param>
/// <param name="basicRepository">basicRepository</param>
/// <param name="publisher">publisher</param>
public class EventSourceProcessInvocable(
    IReadOnlyBasicRepository<EventStream> readOnlyBasicRepository,
    IBasicRepository<EventStream> basicRepository,
    IPublisher publisher)
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
                EventSourceSqlConst.QueryPendingRecordSql,
                new { count = 100, processCount = 3 });
        var enumerable = eventStreams as EventStream[] ?? eventStreams.ToArray();
        if (!enumerable.Any())
        {
            return;
        }

        var successList = new List<int>();
        var failedList = new List<KeyValuePair<int, string>>();
        foreach (var eventStream in enumerable)
        {
            if (string.IsNullOrWhiteSpace(eventStream.FullTypeName))
            {
                failedList.Add(new KeyValuePair<int, string>(eventStream.Id, "事件类型为空"));
            }

            var eventType = Type.GetType(eventStream.FullTypeName!, false);
            if (eventType == null)
            {
                failedList.Add(new KeyValuePair<int, string>(eventStream.Id, "事件类型无法解析"));
            }

            try
            {
                var @event = JsonConvert.DeserializeObject(eventStream.Data, eventType!);
                if (@event == null)
                {
                    failedList.Add(new KeyValuePair<int, string>(eventStream.Id, "事件类型数据为空"));
                }
                else
                {
                    await publisher.Publish(@event);
                    successList.Add(eventStream.Id);
                }
            }
            catch (Exception ex)
            {
                failedList.Add(new KeyValuePair<int, string>(
                    eventStream.Id,
                    TruncateString($"事件类型处理失败:{ex.InnerException?.Message ?? ex.Message}")));
            }
        }

        if (successList.Any())
        {
            await basicRepository.ExecuteUpdateAsync(x => successList.Contains(x.Id), setters => setters
                    .SetProperty(b => b.ProcessCount, b => b.ProcessCount + 1)
                    .SetProperty(b => b.Status, 2)
                    .SetProperty(b => b.LastProcessTime, DateTime.Now)
                    .SetProperty(b => b.Message, "成功"))
                ;
        }

        if (failedList.Any())
        {
            foreach (var keyValuePair in failedList)
            {
                await basicRepository.ExecuteUpdateAsync(x => x.Id == keyValuePair.Key, setters => setters
                    .SetProperty(b => b.ProcessCount, b => b.ProcessCount + 1)
                    .SetProperty(b => b.Status, 3)
                    .SetProperty(b => b.LastProcessTime, DateTime.Now)
                    .SetProperty(b => b.Message, keyValuePair.Value));
            }
        }
    }

    /// <summary>
    ///     截取字符串
    /// </summary>
    /// <param name="input">待处理字符串</param>
    /// <param name="maxLength">最大长度</param>
    /// <returns>处理后的字符串</returns>
    private string TruncateString(string input, int maxLength = 512)
    {
        if (input == null)
        {
            return null;
        }

        return input.Length > maxLength ? input.Substring(0, maxLength) : input;
    }
}