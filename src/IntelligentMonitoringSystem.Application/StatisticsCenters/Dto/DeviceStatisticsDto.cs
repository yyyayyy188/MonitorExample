using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.StatisticsCenters.Dto;

/// <summary>
///     设备统计
/// </summary>
public class DeviceStatisticsDto
{
    /// <summary>
    ///     总数
    /// </summary>
    [JsonProperty("totalCount")]
    public int TotalCount { get; set; }

    /// <summary>
    ///     在线数量
    /// </summary>
    [JsonProperty("onlineCount")]
    public int OnlineCount { get; set; }
}