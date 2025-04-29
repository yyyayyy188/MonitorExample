using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.StatisticsCenters.Dto;

namespace IntelligentMonitoringSystem.Application.StatisticsCenters.StatisticsRanking;

/// <summary>
///     统计排行版查询
/// </summary>
public class StatisticsRankingQuery(string type) : IQuery<List<StatisticsRankingDto>>
{
    /// <summary>
    ///     类型 count | average-time
    /// </summary>
    public string Type { get; set; } = type;
}