using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos.Models;

/// <summary>
///     视频直播流地址请求参数
/// </summary>
public class OpenVideoLiveStreamingRequest
{
    /// <summary>
    ///     设备ID
    /// </summary>
    [JsonProperty("deviceId")]
    public string DeviceId { get; set; }
}