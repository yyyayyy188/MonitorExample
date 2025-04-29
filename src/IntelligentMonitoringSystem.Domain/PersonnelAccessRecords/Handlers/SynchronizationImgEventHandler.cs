using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Events;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Handlers;

/// <summary>
///     上传替换图片原始路径
/// </summary>
public class SynchronizationImgEventHandler(
    IPersonnelAccessRecordFileRepository iPersonnelAccessRecordFileRepository,
    IPersonnelAccessRecordManage personnelAccessRecordManage,
    ILogger<SynchronizationImgEventHandler> logger)
    : INotificationHandler<SynchronizationImgEvent>
{
    /// <summary>
    ///     上传替换图片原始路径
    /// </summary>
    /// <param name="notification">notification</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task Handle(SynchronizationImgEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var faceUploadTask = iPersonnelAccessRecordFileRepository.UploadFile(notification.OriginalFaceUrl);
            var snapUploadTask = iPersonnelAccessRecordFileRepository.UploadFile(notification.OriginalSnapUrl);
            var recognitionFaceTask = iPersonnelAccessRecordFileRepository.UploadFile(notification.RecognitionFaceOssUrl);
            await Task.WhenAll(faceUploadTask, snapUploadTask, recognitionFaceTask);
            await personnelAccessRecordManage.SynchronizationImgAsync(
                notification.EventIdentifier,
                faceUploadTask.Result, snapUploadTask.Result, recognitionFaceTask.Result);
        }
        catch (System.Exception e)
        {
            logger.LogError(e.Message);
        }
    }
}