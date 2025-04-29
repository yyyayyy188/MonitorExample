namespace IntelligentMonitoringSystem.Application.StatisticsCenters.StatisticsRanking;

/// <summary>
///     统计排名
/// </summary>
public class StatisticsRankingPo
{
    /// <summary>
    ///     人脸id
    /// </summary>
    public string FaceId { get; set; }

    /// <summary>
    ///     用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    ///     访问次数
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    ///     总时长
    /// </summary>
    public int TotalTime { get; set; }

    /// <summary>
    ///     平均时长
    /// </summary>
    public int AverageTime { get; set; }
}