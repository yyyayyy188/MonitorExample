using System.Text;
using Dapper;
using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.StatisticsCenters.Dto;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Const;
using IntelligentMonitoringSystem.Infrastructure.Dapper.DbContexts;

namespace IntelligentMonitoringSystem.Application.StatisticsCenters.StatisticsRanking;

/// <summary>
///     统计排名查询处理器
/// </summary>
public class StatisticsRankingQueryHandler(IConnectionFactory<DapperDbContext> connectionFactory)
    : IQueryHandler<StatisticsRankingQuery, List<StatisticsRankingDto>>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>PersonnelAccessRecordDetailDto</returns>
    public async Task<List<StatisticsRankingDto>> Handle(StatisticsRankingQuery request, CancellationToken cancellationToken)
    {
        var dynamicParams = new DynamicParameters();
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(PersonnelAccessRecordSqlConst.StatisticsRankingSql);
        stringBuilder.Append(request.Type.Equals("count") ? " ORDER BY count DESC " : " ORDER BY averageTime DESC ");
        stringBuilder.Append(" LIMIT @limit");
        dynamicParams.Add("limit", 10);
        using var dbConnection = connectionFactory.GetConnection();
        var records = await dbConnection.QueryAsync<StatisticsRankingPo>(
            stringBuilder.ToString(), dynamicParams);
        var abnormalPersonnelAccessRecordPos = records as StatisticsRankingPo[] ?? records.ToArray();
        if (abnormalPersonnelAccessRecordPos.Length == 0)
        {
            return [];
        }

        var result = new List<StatisticsRankingDto>();
        var index = 1;
        foreach (var x in abnormalPersonnelAccessRecordPos)
        {
            result.Add(new StatisticsRankingDto
            {
                Rank = index, Name = x.UserName, Count = x.Count, AverageTime = x.TotalTime / x.Count
            });
            index++;
        }

        return result;
    }
}