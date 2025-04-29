using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.MonitoringCenters.Dtos;

/// <summary>
///     异常人员出入记录统计信息
/// </summary>
public class AbnormalPersonnelAccessRecordStatisticsDto
{
    /// <summary>
    ///     月统计数量
    /// </summary>
    [JsonProperty("monthCount")]
    public int MonthCount { get; set; }

    /// <summary>
    ///     总统计数量
    /// </summary>
    [JsonProperty("totalCount")]
    public int TotalCount { get; set; }

    /// <summary>
    ///     季度统计数量
    /// </summary>
    [JsonProperty("quarterCount")]
    public int QuarterCount { get; set; }

    /// <summary>
    ///     年统计数量
    /// </summary>
    [JsonProperty("yearCount")]
    public int YearCount { get; set; }
}