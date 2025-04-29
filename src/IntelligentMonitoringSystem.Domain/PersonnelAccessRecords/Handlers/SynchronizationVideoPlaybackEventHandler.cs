using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Events;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Configs;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Handlers;

/// <summary>
///     视频回放事件处理器
/// </summary>
public class SynchronizationVideoPlaybackEventHandler(
    IPersonnelAccessRecordFileRepository fileRepository,
    IPersonnelAccessRecordVideoRepository videoRepository,
    IBasicRepository<PersonnelAccessRecord> personnelAccessRecord,
    IOptions<FFMpegOptions> fFMpegOptions,
    ILogger<SynchronizationVideoPlaybackEventHandler> logger,
    ITransformVideoRepository transformVideoRepository)
    : INotificationHandler<SynchronizationVideoPlaybackEvent>
{
    /// <summary>
    ///     考勤事件处理器
    /// </summary>
    /// <param name="notification">notification</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Task</returns>
    public async Task Handle(SynchronizationVideoPlaybackEvent notification, CancellationToken cancellationToken)
    {
        var videoUrl = await videoRepository.GetVideoAsync(notification.DeviceId, notification.AccessTime);
        if (string.IsNullOrWhiteSpace(videoUrl))
        {
            throw new UserFriendlyException("视频回放地址为空");
        }

        var fileName = $"{Guid.NewGuid().ToString().ToLower()}.mp4";
        var filePath = $"{fFMpegOptions.Value.BaseVideoPath}/{fileName}";
        var transformVideoResult = await transformVideoRepository.TransformVideoAsync(videoUrl, filePath);
        if (!transformVideoResult)
        {
            throw new UserFriendlyException("文件转换失败，请重试");
        }

        string uploadVideo;
        await using (var fileStream = new FileStream(filePath, FileMode.Open))
        {
            await using (var memoryStream = new MemoryStream())
            {
                await fileStream.CopyToAsync(memoryStream, cancellationToken);
                memoryStream.Position = 0;
                uploadVideo = await fileRepository.UploadVideo(memoryStream, $"{fileName}");
                if (string.IsNullOrWhiteSpace(uploadVideo))
                {
                    throw new UserFriendlyException("文件上传失败，请重试");
                }
            }
        }

        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        catch (System.Exception ex)
        {
            logger.LogWarning($"删除文件时出错: {ex.Message}");
        }

        logger.LogWarning($"视频转换映射关系为=> videoUrl:{videoUrl},uploadVideo:{uploadVideo}");

        await personnelAccessRecord.ExecuteUpdateAsync(
            x => x.EventIdentifier == notification.EventIdentifier,
            k => k.SetProperty(s => s.DevicePlayback, uploadVideo));
    }
}