using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Dtos;

/// <summary>
///     导出异常人员进出记录
/// </summary>
public class ExportAbnormalPersonnelAccessRequestDto
{
    /// <summary>
    ///     姓名
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>
    ///     状态 In 在院内,Out 在院外
    /// </summary>
    [JsonProperty("status")]
    public string? Status { get; set; }

    /// <summary>
    ///     离开开始时间
    /// </summary>
    [JsonProperty("leaveStartTime")]
    public DateTime? LeaveStartTime { get; set; }

    /// <summary>
    ///     离开结束时间
    /// </summary>
    [JsonProperty("leaveEndTime")]
    public DateTime? LeaveEndTime { get; set; }

    /// <summary>
    ///     返回开始时间
    /// </summary>
    [JsonProperty("enterStartTime")]
    public DateTime? EnterStartTime { get; set; }

    /// <summary>
    ///     返回结束时间
    /// </summary>
    [JsonProperty("enterEndTime")]
    public DateTime? EnterEndTime { get; set; }

    /// <summary>
    ///     ids
    /// </summary>
    [JsonProperty("ids")]
    public List<int>? Ids { get; set; }
}