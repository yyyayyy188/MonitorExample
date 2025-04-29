using Ardalis.SmartEnum.JsonNet;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Dtos;

/// <summary>
///     搜索异常人员出入记录响应参数
/// </summary>
public class SearchAbnormalPersonnelAccessRecordResponseDto
{
    /// <summary>
    ///     Id
    /// </summary>
    [JsonProperty("id")]
    public int Id { get; set; }

    /// <summary>
    ///     姓名
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

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
    ///     当前状态,In为在院内 Out为在院外
    /// </summary>
    [JsonProperty("status")]
    [JsonConverter(typeof(SmartEnumNameConverter<AbnormalPersonnelAccessRecordStatusSmartEnum, string>))]
    public AbnormalPersonnelAccessRecordStatusSmartEnum Status { get; set; }

    /// <summary>
    ///     离开时间
    /// </summary>
    [JsonProperty("leaveTime")]
    public DateTime LeaveTime { get; set; }

    /// <summary>
    ///     返回时间
    /// </summary>
    [JsonProperty("returnTime")]
    public DateTime? ReturnTime { get; set; }

    /// <summary>
    ///     护理人员
    /// </summary>
    [JsonProperty("nurse")]
    public string Nurse { get; set; }

    /// <summary>
    ///     房间号
    /// </summary>
    [JsonProperty("roomNo")]
    public string RoomNo { get; set; }

    /// <summary>
    ///     总时长
    /// </summary>
    [JsonProperty("totalTimes")]
    public long TotalTimes { get; set; }

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
}