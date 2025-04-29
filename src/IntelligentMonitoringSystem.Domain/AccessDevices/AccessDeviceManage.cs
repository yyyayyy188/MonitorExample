using Ardalis.GuardClauses;
using IntelligentMonitoringSystem.Domain.Shared.AccessDevices.Const;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using ZiggyCreatures.Caching.Fusion;

namespace IntelligentMonitoringSystem.Domain.AccessDevices;

/// <summary>
///     设备信息
/// </summary>
public class AccessDeviceManage(IFusionCache fusionCache, IReadOnlyBasicRepository<AccessDevice> basicRepository)
    : IAccessDeviceManage
{
    /// <summary>
    ///     获取访问设备信息
    /// </summary>
    /// <param name="deviceId">设备Id</param>
    /// <returns>AccessDevice</returns>
    public async Task<AccessDevice> GetAccessDeviceByDeviceIdAsync(string deviceId)
    {
        Guard.Against.NullOrEmpty(deviceId);
        var accessDevice = await fusionCache.GetOrSetAsync(
            AccessDeviceConst.GetAccessDeviceCacheKey(deviceId),
            async _ => await GetAccessDeviceByDeviceId(deviceId));
        return accessDevice;
    }

    /// <summary>
    ///     获取访问设备信息
    /// </summary>
    /// <returns>AccessDevice</returns>
    private async Task<AccessDevice> GetAccessDeviceByDeviceId(string deviceId)
    {
        var accessDevice = await basicRepository.QueryFirstOrDefaultAsync(
            AccessDeviceSqlConst.GetAccessDeviceByDeviceId,
            new { deviceId });
        return accessDevice;
    }
}