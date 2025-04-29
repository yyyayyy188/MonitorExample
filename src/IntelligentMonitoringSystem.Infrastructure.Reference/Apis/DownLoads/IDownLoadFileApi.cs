using Refit;

namespace IntelligentMonitoringSystem.Infrastructure.Reference.Apis.DownLoads;

/// <summary>
///     下载文件API
/// </summary>
public interface IDownLoadFileApi
{
    /// <summary>
    ///     下载文件
    /// </summary>
    /// <param name="url">url.</param>
    /// <returns>HttpResponseMessage.</returns>
    [Get("/")]
    Task<HttpResponseMessage> DownloadFileAsync([Property("url")] string url);
}