using Ardalis.SmartEnum.JsonNet;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Events;

/// <summary>
///     WebSocket推送事件
/// </summary>
public class WebSocketNotificationEvent : DomainEventBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="WebSocketNotificationEvent" /> class.
    /// </summary>
    public WebSocketNotificationEvent() { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="WebSocketNotificationEvent" /> class.
    /// </summary>
    /// <param name="eventIdentifier">人员出入记录事件Id</param>
    /// <param name="accessType">入出类型</param>
    public WebSocketNotificationEvent(Guid eventIdentifier, AccessTypeSmartEnum accessType)
    {
        EventIdentifier = eventIdentifier;
        AccessType = accessType;
    }

    /// <summary>
    ///     获取事件流Id
    /// </summary>
    public override string StreamId => EventIdentifier.ToString();

    /// <summary>
    ///     人员出入记录事件Id
    /// </summary>
    public Guid EventIdentifier { get; set; }

    /// <summary>
    ///     入出类型
    /// </summary>
    [JsonConverter(typeof(SmartEnumValueConverter<AccessTypeSmartEnum, string>))]
    public AccessTypeSmartEnum AccessType { get; set; }
}