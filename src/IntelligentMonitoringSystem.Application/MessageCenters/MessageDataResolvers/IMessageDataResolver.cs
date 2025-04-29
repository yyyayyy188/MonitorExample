using IntelligentMonitoringSystem.Domain.Shared.MessageCenters.Enums;

namespace IntelligentMonitoringSystem.Application.MessageCenters.MessageDataResolvers;

/// <summary>
///     消息中心消息解析器
/// </summary>
public interface IMessageDataResolver
{
    /// <summary>
    ///     解析事件源
    /// </summary>
    /// <param name="eventSource">事件源</param>
    /// <param name="data">数据源</param>
    /// <returns>object</returns>
    Task<object> DataResolver(EventSourceSmartEnum eventSource, Dictionary<string, object> data);
}