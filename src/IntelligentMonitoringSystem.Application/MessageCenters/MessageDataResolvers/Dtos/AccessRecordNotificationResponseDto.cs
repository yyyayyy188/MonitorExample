using Ardalis.SmartEnum.JsonNet;
using IntelligentMonitoringSystem.Application.Shared.MessageCenters.Enum;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.MessageCenters.MessageDataResolvers.Dtos;

/// <summary>
///     访问记录消息
/// </summary>
public class AccessRecordNotificationResponseDto(MessageCenterNotifyType notifyType) : BaseMessageDto(notifyType)
{
    /// <summary>
    ///     出入时间
    /// </summary>
    [JsonProperty("accessTime")]
    public string AccessTime { get; set; }

    /// <summary>
    ///     出入类型E为进入,L为离开
    /// </summary>
    [JsonProperty("accessType")]
    [JsonConverter(typeof(SmartEnumNameConverter<AccessTypeSmartEnum, string>))]
    public AccessTypeSmartEnum AccessType { get; set; }

    /// <summary>
    ///     年龄
    /// </summary>
    [JsonProperty("age")]
    public long Age { get; set; }

    /// <summary>
    ///     设备名称
    /// </summary>
    [JsonProperty("deviceName")]
    public string DeviceName { get; set; }

    /// <summary>
    ///     面部抓拍图片地址
    /// </summary>
    [JsonProperty("faceImgUrl")]
    public string FaceImgUrl { get; set; }

    /// <summary>
    ///     性别
    /// </summary>
    [JsonProperty("gender")]
    public string Gender { get; set; }

    /// <summary>
    ///     请假记录Id
    /// </summary>
    [JsonProperty("leaveApplicationId")]
    public int? LeaveApplicationId { get; set; }

    /// <summary>
    ///     请假状态(请假未通过P;未请假W;请假成功S;请假被拒绝F)
    /// </summary>
    [JsonProperty("leaveStatus")]
    [JsonConverter(typeof(SmartEnumNameConverter<LeaveStatusSmartEnum, string>))]
    public LeaveStatusSmartEnum? LeaveStatus { get; set; }

    /// <summary>
    ///     姓名
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    ///     访问记录ID
    /// </summary>
    [JsonProperty("personnelAccessRecordId")]
    public long PersonnelAccessRecordId { get; set; }

    /// <summary>
    ///     抓拍图片地址
    /// </summary>
    [JsonProperty("snapImgPath")]
    public string SnapImgPath { get; set; }
}