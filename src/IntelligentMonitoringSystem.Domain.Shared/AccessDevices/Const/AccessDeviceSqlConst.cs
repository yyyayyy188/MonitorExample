namespace IntelligentMonitoringSystem.Domain.Shared.AccessDevices.Const;

/// <summary>
///     sql
/// </summary>
public static class AccessDeviceSqlConst
{
    /// <summary>
    ///     获取设备信息
    /// </summary>
    public const string GetAccessDeviceByDeviceId = """
                                                    select device_id        as DeviceId,
                                                           cam_model_name   as CamModelName,
                                                           device_direction as AccessDeviceType,
                                                           device_name      as DeviceName,
                                                           device_status    as DeviceStatus,
                                                           device_switch    as DeviceSwitch
                                                    from intelligent_monitoring_system.t_device_info
                                                    where device_id = @deviceId
                                                    limit 1;
                                                    """;

    /// <summary>
    ///     获取设备信息
    /// </summary>
    public const string GetAccessDeviceStatistics = """
                                                    SELECT COUNT(1)                           AS TotalCount,
                                                           SUM(IF(device_status = 1, 1, 0))   AS OnlineCount
                                                    FROM intelligent_monitoring_system.t_device_info
                                                    """;
}