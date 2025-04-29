using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.StatisticsCenters.Dto;

/// <summary>
///     统计排名
/// </summary>
public class StatisticsRankingDto
{
    /// <summary>
    ///     排序
    /// </summary>
    [JsonProperty("rank")]
    public int Rank { get; set; }

    /// <summary>
    ///     名称
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    ///     次数
    /// </summary>
    [JsonProperty("count")]
    public int Count { get; set; }

    /// <summary>
    ///     平均时间(单位秒)
    /// </summary>
    [JsonProperty("averageTime")]
    public int AverageTime { get; set; }
}