using Dapper;
using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.StatisticsCenters.Dto;
using IntelligentMonitoringSystem.Domain.Shared.AccessDevices.Const;
using IntelligentMonitoringSystem.Infrastructure.Dapper.DbContexts;

namespace IntelligentMonitoringSystem.Application.StatisticsCenters.DeviceStatistics;

/// <summary>
///     设备统计
/// </summary>
public class DeviceStatisticsQueryHandler(IConnectionFactory<DapperDbContext> dbContextFactory)
    : IQueryHandler<DeviceStatisticsQuery, DeviceStatisticsDto>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>PersonnelAccessRecordDetailDto</returns>
    public async Task<DeviceStatisticsDto> Handle(DeviceStatisticsQuery request, CancellationToken cancellationToken)
    {
        using var dbConnection = dbContextFactory.GetConnection();
        var result = await dbConnection.QueryFirstOrDefaultAsync<DeviceStatisticsPo>(
            AccessDeviceSqlConst.GetAccessDeviceStatistics);
        return result == null
            ? new DeviceStatisticsDto()
            : new DeviceStatisticsDto { TotalCount = result.TotalCount, OnlineCount = result.OnlineCount };
    }
}