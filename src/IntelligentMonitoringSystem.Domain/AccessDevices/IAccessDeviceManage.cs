namespace IntelligentMonitoringSystem.Domain.AccessDevices;

/// <summary>
///     设备信息
/// </summary>
public interface IAccessDeviceManage
{
    /// <summary>
    ///     获取访问设备信息
    /// </summary>
    /// <param name="deviceId">设备Id</param>
    /// <returns>AccessDevice</returns>
    Task<AccessDevice> GetAccessDeviceByDeviceIdAsync(string deviceId);
}