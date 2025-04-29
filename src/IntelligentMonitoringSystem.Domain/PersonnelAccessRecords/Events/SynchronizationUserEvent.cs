using Ardalis.SmartEnum.JsonNet;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Events;

/// <summary>
///     同步用户事件
/// </summary>
public class SynchronizationUserEvent : DomainEventBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SynchronizationUserEvent" /> class.
    /// </summary>
    public SynchronizationUserEvent()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="SynchronizationUserEvent" /> class.
    ///     同步用户事件
    /// </summary>
    /// <param name="faceId">faceId</param>
    /// <param name="accessType">accessType</param>
    /// <param name="accessTime">accessTime</param>
    public SynchronizationUserEvent(string faceId, AccessTypeSmartEnum accessType, DateTime accessTime)
    {
        FaceId = faceId;
        AccessType = accessType;
        AccessTime = accessTime;
    }

    /// <summary>
    ///     获取事件流Id
    /// </summary>
    public override string StreamId => FaceId;

    /// <summary>
    ///     面容Id
    /// </summary>
    public string FaceId { get; set; }

    /// <summary>
    ///     访问类型
    /// </summary>
    [JsonConverter(typeof(SmartEnumValueConverter<AccessTypeSmartEnum, string>))]
    public AccessTypeSmartEnum AccessType { get; set; }

    /// <summary>
    ///     访问时间
    /// </summary>
    public DateTime AccessTime { get; set; }
}