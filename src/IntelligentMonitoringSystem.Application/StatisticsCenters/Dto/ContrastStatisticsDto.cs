using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.StatisticsCenters.Dto;

/// <summary>
///     对比统计
/// </summary>
public class ContrastStatisticsDto
{
    /// <summary>
    ///     对比-当天
    /// </summary>
    [JsonProperty("day")]
    public ContrastStatisticsDetail Day { get; set; }

    /// <summary>
    ///     对比-当天
    /// </summary>
    [JsonProperty("week")]
    public ContrastStatisticsDetail Week { get; set; }

    /// <summary>
    ///     对比-当天
    /// </summary>
    [JsonProperty("month")]
    public ContrastStatisticsDetail Month { get; set; }
}

/// <summary>
///     对比统计
/// </summary>
public class ContrastStatisticsDetail
{
    /// <summary>
    ///     进入数量
    /// </summary>
    [JsonProperty("enterCount")]
    public int EnterCount { get; set; }

    /// <summary>
    ///     离开数量
    /// </summary>
    [JsonProperty("leaveCount")]
    public int LeaveCount { get; set; }

    /// <summary>
    ///     异常离开数量
    /// </summary>
    [JsonProperty("abnormalLeaveCount")]
    public int AbnormalLeaveCount { get; set; }
}