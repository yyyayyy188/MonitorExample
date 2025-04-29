using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.StatisticsCenters.Dto;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Const;

namespace IntelligentMonitoringSystem.Application.StatisticsCenters.ContrastStatistics;

/// <summary>
///     对比统计查询处理器
/// </summary>
public class ContrastStatisticsQueryHandler(
    IReadOnlyBasicRepository<PersonnelAccessRecordStatistics>
        readOnlyBasicRepository) : IQueryHandler<ContrastStatisticsQuery, ContrastStatisticsDto>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>PersonnelAccessRecordDetailDto</returns>
    public async Task<ContrastStatisticsDto> Handle(ContrastStatisticsQuery request, CancellationToken cancellationToken)
    {
        var filterDateTime = DateTime.Now;
        var dayBeginTime = filterDateTime.Date;
        var weekBeginTime = filterDateTime.Date.AddDays(-6);
        var monthBeginTime = filterDateTime.Date.AddDays(-29);
        var endTime = filterDateTime.Date.AddDays(1).AddSeconds(-1);

        var taskList = new List<Task>();
        var dayTask = readOnlyBasicRepository.QueryFirstOrDefaultAsync(
            PersonnelAccessRecordSqlConst.PersonnelAccessRecordStatisticsSql,
            new { beginTime = dayBeginTime, endTime });
        var weekTask = readOnlyBasicRepository.QueryFirstOrDefaultAsync(
            PersonnelAccessRecordSqlConst.PersonnelAccessRecordStatisticsSql,
            new { beginTime = weekBeginTime, endTime });
        var monthTask = readOnlyBasicRepository.QueryFirstOrDefaultAsync(
            PersonnelAccessRecordSqlConst.PersonnelAccessRecordStatisticsSql,
            new { beginTime = monthBeginTime, endTime });
        taskList.AddRange([dayTask, weekTask, monthTask]);
        await Task.WhenAll(taskList);
        var day = dayTask.Result;
        var week = weekTask.Result;
        var month = monthTask.Result;
        return new ContrastStatisticsDto
        {
            Day = new ContrastStatisticsDetail
            {
                EnterCount = day?.EnterCount ?? 0,
                LeaveCount = day?.LeaveCount ?? 0,
                AbnormalLeaveCount = day?.AbnormalLeaveCount ?? 0
            },
            Week = new ContrastStatisticsDetail
            {
                EnterCount = week?.EnterCount ?? 0,
                LeaveCount = week?.LeaveCount ?? 0,
                AbnormalLeaveCount = week?.AbnormalLeaveCount ?? 0
            },
            Month = new ContrastStatisticsDetail
            {
                EnterCount = month?.EnterCount ?? 0,
                LeaveCount = month?.LeaveCount ?? 0,
                AbnormalLeaveCount = month?.AbnormalLeaveCount ?? 0
            }
        };
    }
}