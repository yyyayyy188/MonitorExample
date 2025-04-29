using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.MonitoringCenters.Dtos;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Const;

namespace IntelligentMonitoringSystem.Application.MonitoringCenters.PersonnelAccessRecordStatistics;

/// <summary>
///     统计查询
/// </summary>
public class PersonnelAccessRecordStatisticsQueryHandler(
    IReadOnlyBasicRepository<Domain.PersonnelAccessRecords.PersonnelAccessRecordStatistics>
        readOnlyBasicRepository)
    : IQueryHandler<PersonnelAccessRecordStatisticsQuery, PersonnelAccessRecordStatisticsDto>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>PersonnelAccessRecordDetailDto</returns>
    public async Task<PersonnelAccessRecordStatisticsDto> Handle(
        PersonnelAccessRecordStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        var filterDateTime = request.FilterDateTime;
        var beginTime = filterDateTime.Date;
        var endTime = filterDateTime.Date.AddDays(1).AddSeconds(-1);
        var personnelAccessRecordStatistics =
            await readOnlyBasicRepository.QueryFirstOrDefaultAsync(
                PersonnelAccessRecordSqlConst.PersonnelAccessRecordStatisticsSql,
                new { beginTime, endTime });
        if (personnelAccessRecordStatistics == null)
        {
            return new PersonnelAccessRecordStatisticsDto();
        }

        return new PersonnelAccessRecordStatisticsDto
        {
            AbnormalLeaveCount = personnelAccessRecordStatistics.AbnormalLeaveCount,
            TotalCount = personnelAccessRecordStatistics.TotalCount,
            EnterCount = personnelAccessRecordStatistics.EnterCount,
            LeaveCount = personnelAccessRecordStatistics.LeaveCount
        };
    }
}