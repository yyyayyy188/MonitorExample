using IntelligentMonitoringSystem.Domain.AccessDevices;
using IntelligentMonitoringSystem.Domain.AccessDevices.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos.Models;

namespace IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos.Repositories;

/// <summary>
///     视频
/// </summary>
/// <param name="videoApi">videoApi</param>
public class AccessDeviceVideoRepository(IVideoApi videoApi) : IAccessDeviceVideoRepository
{
    /// <summary>
    ///     获取视频直播截图
    /// </summary>
    /// <param name="deviceId">设备Id</param>
    /// <returns>视频地址</returns>
    public async Task<AccessDevice> GetVideoLiveStreamingThumbnailAsync(string deviceId)
    {
        var videoLiveStreamingThumbnailResponse =
            await videoApi.GetVideoLiveStreamingThumbnailAsync(new VideoLiveStreamingThumbnailRequest { DeviceId = deviceId });

        if (!videoLiveStreamingThumbnailResponse.IsSuccessful)
        {
            throw new UserFriendlyException($"设备回访查询失败，错误消息：{videoLiveStreamingThumbnailResponse.Content?.ResultMsg}");
        }

        return new AccessDevice
        {
            DeviceId = deviceId,
            VideoLiveStreamingThumbnail = videoLiveStreamingThumbnailResponse.Content?.Data.Url ?? string.Empty
        };
    }
}