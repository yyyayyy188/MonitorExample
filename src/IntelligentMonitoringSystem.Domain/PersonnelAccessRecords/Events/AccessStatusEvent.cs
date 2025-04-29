using Ardalis.SmartEnum.JsonNet;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Events;

/// <summary>
///     异常出入事件
/// </summary>
public class AccessEvent : DomainEventBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AccessEvent" /> class.
    ///     构造函数
    /// </summary>
    public AccessEvent()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="AccessEvent" /> class.
    ///     构造函数
    /// </summary>
    /// <param name="personnelAccessRecordId">personnelAccessRecordId</param>
    /// <param name="originalAccessStatus">originalAccessStatus</param>
    /// <param name="accessStatus">accessStatus</param>
    /// <param name="accessTime">accessTime</param>
    /// <param name="faceId">faceId</param>
    public AccessEvent(int personnelAccessRecordId, AccessStatusSmartEnum originalAccessStatus,
        AccessStatusSmartEnum accessStatus, DateTime accessTime, string faceId)
    {
        PersonnelAccessRecordId = personnelAccessRecordId;
        OriginalAccessStatus = originalAccessStatus;
        AccessStatus = accessStatus;
        AccessTime = accessTime;
        FaceId = faceId;
    }

    /// <summary>
    ///     获取事件流Id
    /// </summary>
    public override string StreamId => PersonnelAccessRecordId.ToString();

    /// <summary>
    ///     访问记录Id
    /// </summary>
    public int PersonnelAccessRecordId { get; set; }

    /// <summary>
    ///     访问时间
    /// </summary>
    public DateTime AccessTime { get; set; }

    /// <summary>
    ///     原始访问访问状态
    /// </summary>
    [JsonConverter(typeof(SmartEnumValueConverter<AccessStatusSmartEnum, string>))]
    public AccessStatusSmartEnum OriginalAccessStatus { get; set; }

    /// <summary>
    ///     访问状态
    /// </summary>
    [JsonConverter(typeof(SmartEnumValueConverter<AccessStatusSmartEnum, string>))]
    public AccessStatusSmartEnum AccessStatus { get; set; }

    /// <summary>
    ///     面容Id
    /// </summary>
    public string FaceId { get; set; }
}