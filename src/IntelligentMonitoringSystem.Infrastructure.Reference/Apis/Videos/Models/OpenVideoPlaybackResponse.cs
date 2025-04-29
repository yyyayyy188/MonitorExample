using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos.Models;

/// <summary>
///     视频播放地址
/// </summary>
public class OpenVideoPlaybackResponse
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
    public OpenVideoBaseResponse Data { get; set; }
}

/// <summary>
///     视频播放地址结果
/// </summary>
public class OpenVideoBaseResponse
{
    /// <summary>
    ///     hls播放地址
    /// </summary>
    [JsonProperty("hlsUrl")]
    public string HlsUrl { get; set; }
}