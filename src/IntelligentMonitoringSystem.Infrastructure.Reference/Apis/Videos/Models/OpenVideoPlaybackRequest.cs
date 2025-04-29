using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos.Models;

/// <summary>
///     视频播放请求参数
/// </summary>
public class OpenVideoPlaybackRequest
{
    /// <summary>
    ///     设备ID
    /// </summary>
    [JsonProperty("deviceId")]
    public string DeviceId { get; set; }

    /// <summary>
    ///     Gets or sets 开始时间
    /// </summary>
    [JsonProperty("startTime")]
    public long StartTime { get; set; }

    /// <summary>
    ///     结束时间
    /// </summary>
    [JsonProperty("endTime")]
    public long EndTime { get; set; }
}