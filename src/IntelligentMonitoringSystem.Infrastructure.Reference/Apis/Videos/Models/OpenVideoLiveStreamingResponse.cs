using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos.Models;

/// <summary>
///     视频直播流地址
/// </summary>
public class OpenVideoLiveStreamingResponse
{
    /// <summary>
    ///     结果代码
    /// </summary>
    [JsonProperty("resultCode")]
    public string ResultCode { get; set; }

    /// <summary>
    ///     结果消息
    /// </summary>
    [JsonProperty("resultMsg")]
    public string ResultMsg { get; set; }

    /// <summary>
    ///     结果
    /// </summary>
    [JsonProperty("data")]
    public OpenVideoLiveStreamingData? Data { get; set; }
}

/// <summary>
///     视频播放地址结果
/// </summary>
public class OpenVideoLiveStreamingData
{
    /// <summary>
    ///     访问路径
    /// </summary>
    [JsonProperty("url")]
    public string Url { get; set; }

    /// <summary>
    ///     过期时间
    /// </summary>
    [JsonProperty("expiresIn")]
    public long ExpiresIn { get; set; }
}