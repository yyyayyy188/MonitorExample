using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.StatisticsCenters.Dto;

/// <summary>
///     趋势统计
/// </summary>
public class TendencyStatisticsDto
{
    /// <summary>
    ///     趋势统计-当天
    /// </summary>
    [JsonProperty("day")]
    public List<TendencyStatisticsOfDay> Day { get; set; } = [];

    /// <summary>
    ///     趋势统计-当天
    /// </summary>
    [JsonProperty("week")]
    public List<TendencyStatisticsOfWeek> Week { get; set; } = [];

    /// <summary>
    ///     趋势统计-当天
    /// </summary>
    [JsonProperty("month")]
    public List<TendencyStatisticsOfMonth> Month { get; set; } = [];
}

/// <summary>
///     趋势统计-月
/// </summary>
public class TendencyStatisticsOfMonth : TendencyStatisticsBase
{
    /// <summary>
    ///     日期
    /// </summary>
    [JsonIgnore]
    public DateTime GroupByKey { get; set; }

    /// <summary>
    ///     日期
    /// </summary>
    [JsonProperty("date")]
    public string Date { get; set; }
}

/// <summary>
///     趋势统计-周
/// </summary>
public class TendencyStatisticsOfWeek : TendencyStatisticsBase
{
    /// <summary>
    ///     日期
    /// </summary>
    [JsonIgnore]
    public DateTime GroupByKey { get; set; }

    /// <summary>
    ///     日期
    /// </summary>
    [JsonProperty("date")]
    public string Date { get; set; }
}

/// <summary>
///     趋势统计-基类
/// </summary>
public class TendencyStatisticsBase
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
}

/// <summary>
///     趋势统计-当天
/// </summary>
public class TendencyStatisticsOfDay : TendencyStatisticsBase
{
    /// <summary>
    ///     分组Key
    /// </summary>
    [JsonIgnore]
    public int GroupByKey { get; set; }

    /// <summary>
    ///     小时
    /// </summary>
    [JsonProperty("hour")]
    public string Hour { get; set; }
}