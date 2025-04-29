using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using IntelligentMonitoringSystem.Domain.Shared.MessageCenters.Enums;

namespace IntelligentMonitoringSystem.Domain.MessageCenters;

/// <summary>
///     消息中心
/// </summary>
public class EventMessage : AggregateRoot
{
    /// <summary>
    ///     事件源
    /// </summary>
    public EventSourceSmartEnum EventSource { get; set; }

    /// <summary>
    ///     事件类型
    /// </summary>
    public Dictionary<string, object> EventSourceData { get; set; }
}