using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;
using IntelligentMonitoringSystem.Infrastructure.Reference.Apis.DownLoads;
using IntelligentMonitoringSystem.Infrastructure.Reference.Apis.FileServers;
using Microsoft.Extensions.Logging;

namespace IntelligentMonitoringSystem.Infrastructure.Reference.PersonnelAccessRecords.Repositories;

/// <summary>
///     考勤记录图片
/// </summary>
public class PersonnelAccessRecordFileRepository(
    IDownLoadFileApi downLoadFileApi,
    IFileServerApi fileServerApi,
    ILogger<IPersonnelAccessRecordFileRepository> logger)
    : IPersonnelAccessRecordFileRepository
{
    /// <summary>
    ///     上传文件
    /// </summary>
    /// <param name="originalFileUrl">originalFileUrl</param>
    /// <returns>string</returns>
    public async Task<string> UploadFile(string originalFileUrl)
    {
        // 下载文件
        var originalDownload = await downLoadFileApi.DownloadFileAsync(originalFileUrl);

        if (originalDownload.IsSuccessStatusCode && originalDownload.Content.Headers.Contains("content-type"))
        {
            var contentType = originalDownload.Content.Headers.GetValues("content-type").FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(contentType) &&
                string.Equals(contentType, "application/octet-stream", StringComparison.OrdinalIgnoreCase))
            {
                // 读取数据流
                var originalDownloadStream = await originalDownload.Content.ReadAsStreamAsync();

                // 转换原始路径
                var originalUri = new Uri(originalFileUrl);
                var fileName = originalUri.LocalPath.Split("/").LastOrDefault() ?? Guid.NewGuid().ToString().ToLower();
                var multipartFormDataContent = new MultipartFormDataContent();
                multipartFormDataContent.Add(new StreamContent(originalDownloadStream), "upload",
                    fileName.Contains(".") ? fileName : $"{fileName}.jpg");
                var result = await fileServerApi.UploadFileAsync(multipartFormDataContent);
                if (!result.IsSuccessful || result.Content?.Code != 200)
                {
                    logger.LogError(result.Error?.Message);
                }

                return result.Content!.Data?.Path ?? string.Empty;
            }
        }

        logger.LogError($"文件下载失败,接口相应：{await originalDownload.Content.ReadAsStringAsync()}");
        return string.Empty;
    }

    /// <summary>
    ///     上传视频
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <param name="fileName">文件名</param>
    /// <returns>文件地址</returns>
    public async Task<string> UploadVideo(Stream stream, string fileName)
    {
        var multipartFormDataContent = new MultipartFormDataContent();
        multipartFormDataContent.Add(new StreamContent(stream), "upload", fileName);
        var result = await fileServerApi.UploadFileAsync(multipartFormDataContent, "video");
        if (!result.IsSuccessful || result.Content?.Code != 200)
        {
            logger.LogError(result.Error?.Message);
        }

        return result.Content!.Data?.Path ?? string.Empty;
    }
}