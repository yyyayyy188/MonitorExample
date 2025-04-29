namespace IntelligentMonitoringSystem.Domain.Shared.AccessDevices.Const;

/// <summary>
///     智能门禁设备常量
/// </summary>
public static class AccessDeviceConst
{
    private const string AccessDeviceCacheKey = "accessDeviceType:{0}";

    /// <summary>
    ///     获取设备缓存Key
    /// </summary>
    /// <param name="deviceId">设备Id</param>
    /// <returns>格式化缓存Key</returns>
    public static string GetAccessDeviceCacheKey(string deviceId)
    {
        return string.Format(AccessDeviceCacheKey, deviceId);
    }
}