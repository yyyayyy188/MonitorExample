using IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos.Models;
using Refit;

namespace IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos;

/// <summary>
///     视频接口
/// </summary>
public interface IVideoApi
{
    /// <summary>
    ///     获取视频回放地址.
    /// </summary>
    /// <param name="request">request.</param>
    /// <param name="apiName">api名称</param>
    /// <returns>HttpResponseMessage.</returns>
    [Post("/gatewaysvc/gateway/openVedio/api/device/hls/palyback")]
    Task<ApiResponse<OpenVideoPlaybackResponse>> GetVideoPlaybackAsync(
        OpenVideoPlaybackRequest request,
        [Property("apiName")] string apiName = "Playback");

    /// <summary>
    ///     获取直播地址.
    /// </summary>
    /// <param name="request">request.</param>
    /// <param name="apiName">api名称</param>
    /// <returns>HttpResponseMessage.</returns>
    [Post("/gatewaysvc/gateway/openVedio/api/websdk/player")]
    Task<ApiResponse<OpenVideoLiveStreamingResponse>> GetVideoLiveStreamingAsync(
        OpenVideoLiveStreamingRequest request,
        [Property("apiName")] string apiName = "LiveStreaming");

    /// <summary>
    ///     获取直播地址.
    /// </summary>
    /// <param name="request">request.</param>
    /// <param name="apiName">api名称</param>
    /// <returns>HttpResponseMessage.</returns>
    [Post("/gatewaysvc/gateway/openVedio/api/camera/thumbnail/realtime")]
    Task<ApiResponse<VideoLiveStreamingThumbnailResponse>> GetVideoLiveStreamingThumbnailAsync(
        VideoLiveStreamingThumbnailRequest request,
        [Property("apiName")] string apiName = "Thumbnail");
}