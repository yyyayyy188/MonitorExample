using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos.Models;

/// <summary>
///     视频直播截图接口返回
/// </summary>
public class VideoLiveStreamingThumbnailResponse
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
    public VideoLiveStreamingThumbnailBaseResponse Data { get; set; }
}

/// <summary>
///     视频直播截图接口返回数据
/// </summary>
public class VideoLiveStreamingThumbnailBaseResponse
{
    /// <summary>
    ///     截图地址
    /// </summary>
    [JsonProperty("url")]
    public string Url { get; set; }
}