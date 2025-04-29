using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using IntelligentMonitoringSystem.Domain.Shared.Extensions;
using IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos;
using IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos.Models;

namespace IntelligentMonitoringSystem.Infrastructure.Reference.PersonnelAccessRecords.Repositories;

/// <summary>
///     视频仓储
/// </summary>
public class PersonnelAccessRecordVideoRepository(IVideoApi videoApi) : IPersonnelAccessRecordVideoRepository
{
    /// <summary>
    ///     获取视频
    /// </summary>
    /// <param name="deviceId">设备Id</param>
    /// <param name="accessTime">访问时间</param>
    /// <returns>视频地址</returns>
    public async Task<string> GetVideoAsync(string deviceId, DateTime accessTime)
    {
        var videoPlaybackResponse = await videoApi.GetVideoPlaybackAsync(new OpenVideoPlaybackRequest
        {
            DeviceId = deviceId,
            StartTime = accessTime.AddSeconds(-60).ToUnixTimestamp(),
            EndTime = accessTime.AddSeconds(60).ToUnixTimestamp()
        });
        if (!videoPlaybackResponse.IsSuccessful)
        {
            throw new UserFriendlyException($"设备回访查询失败，错误消息：{videoPlaybackResponse.Content?.ResultMsg}");
        }

        return videoPlaybackResponse.Content?.Data.HlsUrl ?? string.Empty;
    }
}