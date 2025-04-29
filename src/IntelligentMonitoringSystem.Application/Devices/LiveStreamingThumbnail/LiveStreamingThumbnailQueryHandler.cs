using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Domain.AccessDevices;
using IntelligentMonitoringSystem.Domain.AccessDevices.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;

namespace IntelligentMonitoringSystem.Application.Devices.LiveStreamingThumbnail;

/// <summary>
///     获取设备直播截图
/// </summary>
/// <param name="accessDeviceVideoRepository">设备管理</param>
/// <param name="basicRepository">设备管理仓存</param>
public class LiveStreamingThumbnailQueryHandler(
    IAccessDeviceVideoRepository accessDeviceVideoRepository,
    IBasicRepository<AccessDevice> basicRepository)
    : IQueryHandler<LiveStreamingThumbnailQuery, Dictionary<string, string?>>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>string</returns>
    public async Task<Dictionary<string, string?>> Handle(
        LiveStreamingThumbnailQuery request,
        CancellationToken cancellationToken)
    {
        var accessDevices = await basicRepository.QueryAsync(
            null,
            x => new AccessDevice
            {
                DeviceId = x.DeviceId,
                DeviceName = x.DeviceName,
                CamModelName = x.CamModelName,
                AccessDeviceType = x.AccessDeviceType,
                DeviceStatus = x.DeviceStatus
            });
        var enumerable = accessDevices as AccessDevice[] ?? accessDevices.ToArray();
        var result = new Dictionary<string, string?>();
        if (!enumerable.Any())
        {
            return result;
        }

        var tasks = enumerable.Where(x => x.DeviceStatus == 1).Select(x =>
            accessDeviceVideoRepository.GetVideoLiveStreamingThumbnailAsync(
                x.DeviceId)).ToList();
        await Task.WhenAll(tasks);
        foreach (var accessDevice in tasks.Select(x => x.Result).ToList())
        {
            result.Add(accessDevice.DeviceId, accessDevice.VideoLiveStreamingThumbnail);
        }

        foreach (var accessDevice in enumerable.Where(x => x.DeviceStatus == 0))
        {
            result.Add(accessDevice.DeviceId, null);
        }

        return result.OrderBy(x => x.Key).ToDictionary();
    }
}