using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Domain.AccessDevices;
using IntelligentMonitoringSystem.Domain.LeaveApplications;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;
using Microsoft.Extensions.Logging;

namespace IntelligentMonitoringSystem.Application.AlarmEventMessages.Create;

/// <summary>
///     告警事件消息
/// </summary>
public class AlarmEventMessageCommandHandler(
    IPersonnelAccessRecordManage personnelAccessRecordManage,
    ILeaveApplicationManage leaveApplicationManage,
    IAccessDeviceManage accessDeviceManage,
    ILogger<AlarmEventMessageCommandHandler> logger)
    : ICommandHandler<AlarmEventMessageCommand>
{
    /// <summary>
    ///     处理告警事件消息
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Task</returns>
    public async Task Handle(AlarmEventMessageCommand request, CancellationToken cancellationToken)
    {
        var leaveApplication = await leaveApplicationManage.GetLeaveApplicationByFaceIdAsync(request.AlarmContext.FaceId);
        var accessDevice = await accessDeviceManage.GetAccessDeviceByDeviceIdAsync(request.DeviceId);

        if (accessDevice == null)
        {
            logger.LogWarning($"the access device is not found, the deviceId is {request.DeviceId}");
            return;
        }

        var personnelAccessRecord = new PersonnelAccessRecord(accessDevice, request.AlarmContext.FaceId!,
            request.AlarmContext.Name!, request.AlarmContext.Age,
            request.AlarmContext.Gender!, request.AlarmContext.SnapTime, request.AlarmContext.SnapUrl!,
            request.AlarmContext.SnapUrl!, request.AlarmContext.RecognitionFaceOssUrl, leaveApplication);

        // 设置日志信息
        personnelAccessRecord.SetTopicAlarmEventLog(request.AppId, request.MsgType, request.Sig, request.DeviceId,
            request.AlarmContext.SnapTime, request.OriginalAlarmContext, request.DeviceName, request.DeviceAddress,
            request.StoreName, request.StoreId, request.RegionName, request.Region);
        await personnelAccessRecordManage.SaveAsync(personnelAccessRecord);
    }
}