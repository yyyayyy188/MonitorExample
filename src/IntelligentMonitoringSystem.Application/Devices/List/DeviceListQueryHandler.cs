using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.Devices.Dtos;
using IntelligentMonitoringSystem.Application.Shared.Dto.Lists;
using IntelligentMonitoringSystem.Domain.AccessDevices;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;

namespace IntelligentMonitoringSystem.Application.Devices.List;

/// <summary>
///     设备列表查询
/// </summary>
public class DeviceListQueryHandler(IBasicRepository<AccessDevice> basicRepository)
    : IQueryHandler<DeviceListQuery, MonitorCenterDeviceDto>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>string</returns>
    public async Task<MonitorCenterDeviceDto> Handle(DeviceListQuery request, CancellationToken cancellationToken)
    {
        var accessDevices = await basicRepository.QueryAsync(
            null,
            x => new AccessDevice
            {
                DeviceId = x.DeviceId,
                DeviceName = x.DeviceName,
                CamModelName = x.CamModelName,
                AccessDeviceType = x.AccessDeviceType,
                DeviceStatus = x.DeviceStatus,
                UpdateTime = x.UpdateTime
            });
        var enumerable = accessDevices as AccessDevice[] ?? accessDevices.ToArray();
        var monitorCenterDevice = new MonitorCenterDeviceDto
        {
            TotalCount = enumerable.Length,
            LiveCount = enumerable.Count(x => x.DeviceStatus == 1),
            LastUpdateTime = enumerable.FirstOrDefault()?.UpdateTime,
            List = new ListResultDto<DeviceInfoDto>(enumerable.Select(x => new DeviceInfoDto
            {
                DeviceId = x.DeviceId,
                DeviceName = x.DeviceName,
                CamModelName = x.CamModelName,
                AccessDeviceType = x.AccessDeviceType,
                DeviceStatus = x.DeviceStatus
            }).OrderBy(x => x.DeviceId).ToList())
        };
        return monitorCenterDevice;
    }
}