using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos.Models;

/// <summary>
///     获取视频直播截图请求参数
/// </summary>
public class VideoLiveStreamingThumbnailRequest
{
    /// <summary>
    ///     设备ID
    /// </summary>
    [JsonProperty("deviceId")]
    public string DeviceId { get; set; }
}