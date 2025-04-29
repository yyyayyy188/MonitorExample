using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Domain.AccessDevices;
using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using IntelligentMonitoringSystem.Domain.Shared.Extensions;
using IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos;
using IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos.Models;

namespace IntelligentMonitoringSystem.Application.Devices.VideoPlayback;

/// <summary>
///     视频回放
/// </summary>
public class VideoPlaybackQueryHandler(IVideoApi videoApi, IAccessDeviceManage accessDeviceManage)
    : IQueryHandler<VideoPlaybackQuery, string>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>string</returns>
    public async Task<string> Handle(VideoPlaybackQuery request, CancellationToken cancellationToken)
    {
        var accessDevice = await accessDeviceManage.GetAccessDeviceByDeviceIdAsync(request.DeviceId);
        if (accessDevice == null)
        {
            throw new UserFriendlyException($"设备：{request.DeviceId}信息不存在");
        }

        var videoPlaybackResponse = await videoApi.GetVideoPlaybackAsync(new OpenVideoPlaybackRequest
        {
            DeviceId = request.DeviceId,
            StartTime = request.BeginTime.ToUnixTimestamp(),
            EndTime = request.EndTime.ToUnixTimestamp()
        });
        if (!videoPlaybackResponse.IsSuccessful)
        {
            throw new UserFriendlyException($"设备回访查询失败，错误消息：{videoPlaybackResponse.Content?.ResultMsg}");
        }

        return videoPlaybackResponse.Content?.Data.HlsUrl ?? string.Empty;
    }
}