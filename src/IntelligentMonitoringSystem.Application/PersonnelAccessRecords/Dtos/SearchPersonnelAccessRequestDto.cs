using IntelligentMonitoringSystem.Application.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Dtos;

/// <summary>
///     搜索出入记录
/// </summary>
public class SearchPersonnelAccessRequestDto : PageSearchRequestDto
{
    /// <summary>
    ///     姓名
    /// </summary>
    [JsonProperty("name")]
    [FromQuery(Name = "name")]
    public string? Name { get; set; }

    /// <summary>
    ///     出入状态 Abnormal:异常 Normal:正常 Pending:暂无
    /// </summary>
    [JsonProperty("accessStatus")]
    [FromQuery(Name = "accessStatus")]
    public string? AccessStatus { get; set; }

    /// <summary>
    ///     开始事件
    /// </summary>
    [JsonProperty("startTime")]
    [FromQuery(Name = "startTime")]
    public DateTime? StartTime { get; set; }

    /// <summary>
    ///     结束时间
    /// </summary>
    [JsonProperty("endTime")]
    [FromQuery(Name = "endTime")]
    public DateTime? EndTime { get; set; }
}