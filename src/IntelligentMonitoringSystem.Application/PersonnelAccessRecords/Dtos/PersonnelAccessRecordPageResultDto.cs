using Ardalis.SmartEnum.JsonNet;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Dtos;

/// <summary>
///     搜索人员出入记录返回
/// </summary>
public class SearchPersonnelAccessRecordResponseDto
{
    /// <summary>
    ///     Id
    /// </summary>
    [JsonProperty("personnelAccessRecordId")]
    public int PersonnelAccessRecordId { get; set; }

    /// <summary>
    ///     姓名
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>
    ///     年龄
    /// </summary>
    [JsonProperty("age")]
    public short Age { get; set; }

    /// <summary>
    ///     性别
    /// </summary>
    [JsonProperty("gender")]
    public string Gender { get; set; }

    /// <summary>
    ///     出入时间
    /// </summary>
    [JsonProperty("accessTime")]
    public DateTime? AccessTime { get; set; }

    /// <summary>
    ///     出入状态
    /// </summary>
    [JsonProperty("accessStatus")]
    [JsonConverter(typeof(SmartEnumNameConverter<AccessStatusSmartEnum, string>))]
    public AccessStatusSmartEnum AccessStatus { get; set; }

    /// <summary>
    ///     出入类型
    /// </summary>
    [JsonProperty("accessType")]
    [JsonConverter(typeof(SmartEnumNameConverter<AccessTypeSmartEnum, string>))]
    public AccessTypeSmartEnum AccessType { get; set; }

    /// <summary>
    ///     请假状态(W:未请假,S:请假通过,F请假未通过)
    /// </summary>
    [JsonProperty("leaveStatus")]
    [JsonConverter(typeof(SmartEnumNameConverter<LeaveStatusSmartEnum, string>))]
    public LeaveStatusSmartEnum? LeaveStatus { get; set; }

    /// <summary>
    ///     请假记录Id
    /// </summary>
    [JsonProperty("leaveApplicationId")]
    public int? LeaveApplicationId { get; set; }

    /// <summary>
    ///     头像图片路径
    /// </summary>
    [JsonProperty("faceImgPath")]
    public string? FaceImgPath { get; set; }

    /// <summary>
    ///     抓拍图片路径
    /// </summary>
    [JsonProperty("snapImgPath")]
    public string? SnapImgPath { get; set; }
}