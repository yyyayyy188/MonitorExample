using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.Devices.Dtos;

namespace IntelligentMonitoringSystem.Application.Devices.List;

/// <summary>
///     设备列表查询
/// </summary>
public class DeviceListQuery : IQuery<MonitorCenterDeviceDto>;