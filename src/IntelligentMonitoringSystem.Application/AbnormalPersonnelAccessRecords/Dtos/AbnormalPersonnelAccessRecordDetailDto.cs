using Ardalis.SmartEnum.JsonNet;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Dtos;

/// <summary>
///     异常出入记录详情
/// </summary>
public class AbnormalPersonnelAccessRecordDetailDto
{
    /// <summary>
    ///     Id
    /// </summary>
    [JsonProperty("id")]
    public int Id { get; set; }

    /// <summary>
    ///     PersonnelAccessRecordId
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
    ///     当前状态,In为在院内 Out为在院外
    /// </summary>
    [JsonProperty("status")]
    [JsonConverter(typeof(SmartEnumNameConverter<AbnormalPersonnelAccessRecordStatusSmartEnum, string>))]
    public AbnormalPersonnelAccessRecordStatusSmartEnum Status { get; set; }

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

    /// <summary>
    ///     备注信息
    /// </summary>
    [JsonProperty("remark")]
    public string? Remark { get; set; }

    /// <summary>
    ///     设备Id
    /// </summary>
    [JsonProperty("deviceId")]
    public string DeviceId { get; set; }

    /// <summary>
    ///     设备名称
    /// </summary>
    [JsonProperty("deviceName")]
    public string DeviceName { get; set; }

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
    ///     视频回访地址
    /// </summary>
    [JsonProperty("videoPlayback")]
    public string? VideoPlayback { get; set; }

    /// <summary>
    ///     总时长
    /// </summary>
    [JsonProperty("totalTimes")]
    public long TotalTimes { get; set; }
}