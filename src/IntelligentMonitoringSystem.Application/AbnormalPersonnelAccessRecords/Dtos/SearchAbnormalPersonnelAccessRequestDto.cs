using IntelligentMonitoringSystem.Application.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Dtos;

/// <summary>
///     搜索异常人员出入记录请求
/// </summary>
public class SearchAbnormalPersonnelAccessRequestDto : PageSearchRequestDto
{
    /// <summary>
    ///     姓名
    /// </summary>
    [JsonProperty("name")]
    [FromQuery(Name = "name")]
    public string? Name { get; set; }

    /// <summary>
    ///     状态 In 在院内,Out 在院外
    /// </summary>
    [JsonProperty("status")]
    [FromQuery(Name = "status")]
    public string? Status { get; set; }

    /// <summary>
    ///     离开开始时间
    /// </summary>
    [JsonProperty("leaveStartTime")]
    [FromQuery(Name = "leaveStartTime")]
    public DateTime? LeaveStartTime { get; set; }

    /// <summary>
    ///     离开结束时间
    /// </summary>
    [JsonProperty("leaveEndTime")]
    [FromQuery(Name = "leaveEndTime")]
    public DateTime? LeaveEndTime { get; set; }

    /// <summary>
    ///     返回开始时间
    /// </summary>
    [JsonProperty("enterStartTime")]
    [FromQuery(Name = "enterStartTime")]
    public DateTime? EnterStartTime { get; set; }

    /// <summary>
    ///     返回结束时间
    /// </summary>
    [JsonProperty("enterEndTime")]
    [FromQuery(Name = "enterEndTime")]
    public DateTime? EnterEndTime { get; set; }
}