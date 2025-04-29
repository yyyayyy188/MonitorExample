using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Domain.Shared.MessageCenters.Enums;

namespace IntelligentMonitoringSystem.Application.MessageCenters.WebSocket;

/// <summary>
///     WebSocket消息
/// </summary>
public class WebSocketMessageQuery(EventSourceSmartEnum eventSource, Dictionary<string, object> eventSourceData)
    : QueryBase<string>
{
    /// <summary>
    ///     事件源
    /// </summary>
    public EventSourceSmartEnum EventSource { get; set; } = eventSource;

    /// <summary>
    ///     事件源数据
    /// </summary>
    public Dictionary<string, object> EventSourceData { get; set; } = eventSourceData;
}