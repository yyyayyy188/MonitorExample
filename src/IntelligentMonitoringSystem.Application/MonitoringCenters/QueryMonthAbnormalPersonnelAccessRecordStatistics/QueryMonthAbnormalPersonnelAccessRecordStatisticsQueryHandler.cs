using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Const;

namespace IntelligentMonitoringSystem.Application.MonitoringCenters.QueryMonthAbnormalPersonnelAccessRecordStatistics;

/// <summary>
///     月度异常人员出入记录查询
/// </summary>
/// <param name="readOnlyBasicRepository">readOnlyBasicRepository</param>
public class QueryMonthAbnormalPersonnelAccessRecordStatisticsQueryHandler(
    IReadOnlyBasicRepository<AbnormalPersonnelAccessRecord>
        readOnlyBasicRepository)
    : IQueryHandler<QueryMonthAbnormalPersonnelAccessRecordStatisticsQuery, List<int>>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>PersonnelAccessRecordDetailDto</returns>
    public async Task<List<int>> Handle(
        QueryMonthAbnormalPersonnelAccessRecordStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        var filterDateTime = request.FilterMonthTime;
        var beginTime = new DateTime(filterDateTime.Year, filterDateTime.Month, 1);
        var endTime = beginTime.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
        var personnelAccessRecordStatistics =
            await readOnlyBasicRepository.QueryAsync(
                PersonnelAccessRecordSqlConst.QueryMonthAbnormalRecordStatisticsSql,
                new { beginTime, endTime });
        var abnormalPersonnelAccessRecords = personnelAccessRecordStatistics as AbnormalPersonnelAccessRecord[] ??
                                             personnelAccessRecordStatistics.ToArray();
        return !abnormalPersonnelAccessRecords.Any()
            ? []
            : abnormalPersonnelAccessRecords.Select(x => x.LeaveTime.Day).Distinct().ToList();
    }
}