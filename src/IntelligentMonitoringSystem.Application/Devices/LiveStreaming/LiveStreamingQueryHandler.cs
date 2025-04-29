using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Domain.AccessDevices;
using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos;
using IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos.Models;

namespace IntelligentMonitoringSystem.Application.Devices.LiveStreaming;

/// <summary>
///     直播流
/// </summary>
/// <param name="videoApi">videoApi</param>
/// <param name="accessDeviceManage">accessDeviceManage</param>
public class LiveStreamingQueryHandler(IVideoApi videoApi, IAccessDeviceManage accessDeviceManage)
    : IQueryHandler<LiveStreamingQuery, string>
{
    /// <summary>
    ///     获取设备直播流地址
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Task<string /></returns>
    /// <exception cref="UserFriendlyException">UserFriendlyException</exception>
    public async Task<string> Handle(LiveStreamingQuery request, CancellationToken cancellationToken)
    {
        var accessDevice = await accessDeviceManage.GetAccessDeviceByDeviceIdAsync(request.DeviceId);
        if (accessDevice == null)
        {
            throw new UserFriendlyException($"设备：{request.DeviceId}信息不存在");
        }

        var videoLiveStreamingResponse =
            await videoApi.GetVideoLiveStreamingAsync(new OpenVideoLiveStreamingRequest { DeviceId = request.DeviceId });
        if (!videoLiveStreamingResponse.IsSuccessful)
        {
            throw new UserFriendlyException($"设备回访查询失败，错误消息：{videoLiveStreamingResponse.Content?.ResultMsg}");
        }

        return videoLiveStreamingResponse.Content?.Data?.Url ?? string.Empty;
    }
}