namespace IntelligentMonitoringSystem.Domain.AccessDevices.Repositories;

/// <summary>
///     视频设备仓储
/// </summary>
public interface IAccessDeviceVideoRepository
{
    /// <summary>
    ///     获取设备直播流截图
    /// </summary>
    /// <param name="deviceId">设备Id</param>
    /// <returns>截图地址</returns>
    Task<AccessDevice> GetVideoLiveStreamingThumbnailAsync(string deviceId);
}