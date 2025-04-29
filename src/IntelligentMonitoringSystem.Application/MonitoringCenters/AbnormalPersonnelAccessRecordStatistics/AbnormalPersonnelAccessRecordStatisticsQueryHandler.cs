using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.MonitoringCenters.Dtos;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Const;

namespace IntelligentMonitoringSystem.Application.MonitoringCenters.AbnormalPersonnelAccessRecordStatistics;

/// <summary>
///     获取异常人员通行记录统计信息
/// </summary>
public class AbnormalPersonnelAccessRecordStatisticsQueryHandler(
    IReadOnlyBasicRepository<Domain.PersonnelAccessRecords.AbnormalPersonnelAccessRecordStatistics> readOnlyBasicRepository)
    : IQueryHandler<AbnormalPersonnelAccessRecordStatisticsQuery,
        AbnormalPersonnelAccessRecordStatisticsDto>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>PersonnelAccessRecordDetailDto</returns>
    public async Task<AbnormalPersonnelAccessRecordStatisticsDto> Handle(
        AbnormalPersonnelAccessRecordStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        var abnormalPersonnelAccessRecordStatistics = await readOnlyBasicRepository.QueryFirstOrDefaultAsync(
            PersonnelAccessRecordSqlConst.AbnormalPersonnelAccessRecordStatisticsSql,
            new { filterTime = DateTime.Now });
        if (abnormalPersonnelAccessRecordStatistics == null)
        {
            return new AbnormalPersonnelAccessRecordStatisticsDto();
        }

        return new AbnormalPersonnelAccessRecordStatisticsDto
        {
            YearCount = abnormalPersonnelAccessRecordStatistics.YearCount,
            QuarterCount = abnormalPersonnelAccessRecordStatistics.QuarterCount,
            MonthCount = abnormalPersonnelAccessRecordStatistics.MonthCount,
            TotalCount = abnormalPersonnelAccessRecordStatistics.TotalCount
        };
    }
}