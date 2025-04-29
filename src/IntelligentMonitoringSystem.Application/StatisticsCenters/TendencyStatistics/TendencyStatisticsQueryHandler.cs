using Dapper;
using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.StatisticsCenters.Dto;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using IntelligentMonitoringSystem.Domain.Shared.StatisticsCenters.Const;
using IntelligentMonitoringSystem.Infrastructure.Dapper.DbContexts;

namespace IntelligentMonitoringSystem.Application.StatisticsCenters.TendencyStatistics;

/// <summary>
///     趋势统计查询处理器
/// </summary>
public class TendencyStatisticsQueryHandler(IConnectionFactory<DapperDbContext> connectionFactory)
    : IQueryHandler<TendencyStatisticsQuery, TendencyStatisticsDto>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>PersonnelAccessRecordDetailDto</returns>
    public async Task<TendencyStatisticsDto> Handle(TendencyStatisticsQuery request, CancellationToken cancellationToken)
    {
        var currentDateTime = DateTime.Now;
        TendencyStatisticsDto dto = new();
        using var dbConnection = connectionFactory.GetConnection();
        await using var queryMultiple = await dbConnection.QueryMultipleAsync(
            StatisticsCentersSqlConst.TendencyStatisticsSql,
            new
            {
                currentStartOfDay = currentDateTime.Date,
                currentEndOfDay = currentDateTime.Date.AddDays(1).AddSeconds(-1),
                dayOfWeekStart = currentDateTime.Date.AddDays(-6),
                dayOfMonthStart = currentDateTime.Date.AddDays(-29)
            });

        var hourStatistics = await queryMultiple.ReadAsync<TendencyStatisticsPo>();
        var tendencyStatisticsPos = hourStatistics as TendencyStatisticsPo[] ?? hourStatistics.ToArray();
        if (tendencyStatisticsPos.Any())
        {
            dto.Day.AddRange(tendencyStatisticsPos
                .GroupBy(po => po.Hour)
                .Select(x => new TendencyStatisticsOfDay
                {
                    GroupByKey = x.Key,
                    Hour = FormatHour(x.Key),
                    EnterCount = x.Where(po => po.AccessType == AccessTypeSmartEnum.Enter).Sum(po => po.Count),
                    LeaveCount = x.Where(po => po.AccessType == AccessTypeSmartEnum.Leave).Sum(po => po.Count)
                }).ToList());
        }

        var alreadyExists = dto.Day.Select(x => x.GroupByKey).ToList();
        dto.Day.AddRange(Enumerable.Range(0, 24).Where(x => !alreadyExists.Contains(x)).Select(x =>
            new TendencyStatisticsOfDay { GroupByKey = x, Hour = FormatHour(x), EnterCount = 0, LeaveCount = 0 }));
        dto.Day = dto.Day.OrderBy(x => x.GroupByKey).ToList();
        var dayOfMonthStatistics = await queryMultiple.ReadAsync<TendencyStatisticsPo>();
        var dayOfMonthStatisticsArray = dayOfMonthStatistics as TendencyStatisticsPo[] ?? dayOfMonthStatistics.ToArray();
        if (dayOfMonthStatisticsArray.Any())
        {
            // 周数据处理
            dto.Week.AddRange(
                dayOfMonthStatisticsArray.Where(x => x.DayOfMonth >= currentDateTime.Date.AddDays(-6))
                    .GroupBy(po => po.DayOfMonth)
                    .Select(x => new TendencyStatisticsOfWeek
                    {
                        GroupByKey = x.Key,
                        Date = FormatDate(x.Key),
                        EnterCount = x.Where(po => po.AccessType == AccessTypeSmartEnum.Enter).Sum(po => po.Count),
                        LeaveCount = x.Where(po => po.AccessType == AccessTypeSmartEnum.Leave).Sum(po => po.Count)
                    }).ToList());

            // 月数据处理
            dto.Month.AddRange(
                dayOfMonthStatisticsArray
                    .GroupBy(po => po.DayOfMonth)
                    .Select(x => new TendencyStatisticsOfMonth
                    {
                        GroupByKey = x.Key,
                        Date = FormatDate(x.Key),
                        EnterCount = x.Where(po => po.AccessType == AccessTypeSmartEnum.Enter).Sum(po => po.Count),
                        LeaveCount = x.Where(po => po.AccessType == AccessTypeSmartEnum.Leave).Sum(po => po.Count)
                    }).ToList());
        }

        var weekAlreadyExists = dto.Week.Select(x => x.GroupByKey).ToList();
        dto.Week.AddRange(
            Enumerable.Range(0, 7).Select(x => DateTime.Now.Date.AddDays(-x)).Where(x => !weekAlreadyExists.Contains(x))
                .Select(
                    x => new TendencyStatisticsOfWeek { GroupByKey = x, Date = FormatDate(x), EnterCount = 0, LeaveCount = 0 })
                .ToList());
        dto.Week = dto.Week.OrderBy(x => x.GroupByKey).ToList();

        var monthAlreadyExists = dto.Month.Select(x => x.GroupByKey).ToList();
        dto.Month.AddRange(
            Enumerable.Range(0, 30).Select(x => DateTime.Now.Date.AddDays(-x)).Where(x => !monthAlreadyExists.Contains(x))
                .Select(x => new TendencyStatisticsOfMonth
                {
                    GroupByKey = x, Date = FormatDate(x), EnterCount = 0, LeaveCount = 0
                }).ToList());
        dto.Month = dto.Month.OrderBy(x => x.GroupByKey).ToList();
        return dto;
    }

    /// <summary>
    ///     格式化小时
    /// </summary>
    /// <param name="hour">hour</param>
    /// <returns>string</returns>
    private string FormatHour(int hour)
    {
        return hour < 10 ? $"0{hour}" : hour.ToString();
    }

    /// <summary>
    ///     格式化日期
    /// </summary>
    /// <param name="date">date</param>
    /// <returns>string</returns>
    private string FormatDate(DateTime date)
    {
        return date.ToString("MM/dd");
    }
}