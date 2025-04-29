using Ardalis.SmartEnum.JsonNet;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Events;

/// <summary>
///     检测是否为异常离场事件
/// </summary>
public class CheckIsAbnormalLeaveEvent
    : DomainEventBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CheckIsAbnormalLeaveEvent" /> class.
    ///     构造函数
    /// </summary>
    public CheckIsAbnormalLeaveEvent()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="CheckIsAbnormalLeaveEvent" /> class.
    ///     构造函数
    /// </summary>
    /// <param name="eventIdentifier">eventIdentifier</param>
    /// <param name="accessType">accessType</param>
    /// <param name="faceId">faceId</param>
    /// <param name="accessTime">accessTime</param>
    public CheckIsAbnormalLeaveEvent(Guid eventIdentifier, AccessTypeSmartEnum accessType, string faceId, DateTime accessTime)
    {
        EventIdentifier = eventIdentifier;
        AccessType = accessType;
        FaceId = faceId;
        AccessTime = accessTime;
    }

    /// <summary>
    ///     人员出入记录事件Id
    /// </summary>
    public Guid EventIdentifier { get; set; }

    /// <summary>
    ///     入出类型
    /// </summary>
    [JsonConverter(typeof(SmartEnumValueConverter<AccessTypeSmartEnum, string>))]
    public AccessTypeSmartEnum AccessType { get; set; }

    /// <summary>
    ///     进出时间
    /// </summary>
    public DateTime AccessTime { get; set; }

    /// <summary>
    ///     人脸Id
    /// </summary>
    public string FaceId { get; set; }

    /// <summary>
    ///     获取事件流Id
    /// </summary>
    public override string StreamId => EventIdentifier.ToString();
}