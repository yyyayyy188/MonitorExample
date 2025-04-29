#nullable enable
using Ardalis.GuardClauses;
using IntelligentMonitoringSystem.Domain.AccessDevices;
using IntelligentMonitoringSystem.Domain.LeaveApplications;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Events;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Exception;
using IntelligentMonitoringSystem.Domain.Shared.Configs;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;

/// <summary>
///     出入记录业务方法
/// </summary>
public partial class PersonnelAccessRecord
{
    /// <summary>
    ///     设置主题报警事件
    /// </summary>
    /// <param name="appId">appId</param>
    /// <param name="msgType">msgType</param>
    /// <param name="sig">sig</param>
    /// <param name="deviceId">deviceId</param>
    /// <param name="detectTime">detectTime</param>
    /// <param name="alarmContext">alarmContext</param>
    /// <param name="deviceName">设备名称</param>
    /// <param name="deviceAddress">设备地址</param>
    /// <param name="storeName">店铺名称</param>
    /// <param name="storeId">店铺Id</param>
    /// <param name="regionName">区域名称</param>
    /// <param name="region">区域</param>
    public void SetTopicAlarmEventLog(string appId, string msgType, string sig, string deviceId,
        DateTime detectTime, string alarmContext, string deviceName, string deviceAddress, string storeName,
        string storeId, string regionName, string region)
    {
        TopicAlarmEventLog = new TopicAlarmEventLog(appId, msgType, detectTime, sig, deviceId, detectTime,
            alarmContext, deviceName, deviceAddress, storeName, storeId, regionName, region);
    }

    /// <summary>
    ///     设置关联用户访问记录Id
    /// </summary>
    public void SetPersonnelAccessRecordId()
    {
        TopicAlarmEventLog.PersonnelAccessRecordId = Id;
    }

    /// <summary>
    ///     设置离开访问状态
    /// </summary>
    /// <param name="accessStatus">出入状态</param>
    /// <param name="remark">备注</param>
    /// <param name="userId">用户id</param>
    public void SetLeaveAccessStatus(AccessStatusSmartEnum accessStatus, string remark, string userId)
    {
        if (AccessType != AccessTypeSmartEnum.Leave)
        {
            throw new AccessStatusNotAllowEditException();
        }

        if (!string.IsNullOrWhiteSpace(remark))
        {
            Remark = Guard.Against.StringTooLong(remark, 100, message: "备注信息长度不能超过100");
        }

        var originalAccessStatus = AccessStatus;
        AddEvent(new DomainEventRecord(new AccessEvent(Id, originalAccessStatus, accessStatus, AccessTime, FaceId), true));
        AccessStatus = accessStatus;

        // 审计事件
        LastEditUserId = userId;
        LastEditTime = DateTime.Now;
        LastEditUserName = userId;
    }

    /// <summary>
    ///     设置进入访问状态
    /// </summary>
    /// <param name="remark">备注</param>
    /// <param name="userId">用户id</param>
    public void SetEnterAccessStatus(string remark, string userId)
    {
        if (!string.IsNullOrWhiteSpace(remark))
        {
            Remark = Guard.Against.StringTooLong(remark, 100, message: "备注信息长度不能超过100");
        }

        // 审计事件
        LastEditUserId = userId;
        LastEditTime = DateTime.Now;
        LastEditUserName = userId;
    }

    /// <summary>
    ///     设置设备信息
    /// </summary>
    /// <param name="accessDevice">accessDevice.</param>
    private void SetDeviceInfo(AccessDevice accessDevice)
    {
        Guard.Against.Null(accessDevice, nameof(accessDevice));
        DeviceId = accessDevice.DeviceId;
        DeviceName = accessDevice.DeviceName;
        AccessType = accessDevice.AccessDeviceType.GetAccessType();
        AddEvent(new DomainEventRecord(new WebSocketNotificationEvent(EventIdentifier, AccessType), true));
        AddEvent(new DomainEventRecord(
            new SynchronizationVideoPlaybackEvent(EventIdentifier, accessDevice.DeviceId, AccessTime)));
        if (AccessType == AccessTypeSmartEnum.Leave)
        {
            AccessStatus = AccessStatusSmartEnum.Pending;
        }
        else
        {
            AccessStatus = AccessStatusSmartEnum.Normal;

            // 发送消息Check是否存在异常外出记录，如果存在弹窗提醒，并且如果未超过2小时则取消后续弹窗提醒
            AddEvent(new DomainEventRecord(
                new CheckIsAbnormalLeaveEvent(EventIdentifier, AccessType,
                    FaceId, AccessTime), true));
        }
    }

    /// <summary>
    ///     设置原始图片路径
    /// </summary>
    /// <param name="originalSnapUrl">原始抓拍图片</param>
    /// <param name="originalFaceUrl">原始头像图片</param>
    /// <param name="recognitionFaceOssUrl">原始采集图片地址</param>
    private void SetOriginalImgPath(string originalSnapUrl, string originalFaceUrl, string recognitionFaceOssUrl)
    {
        OriginalSnapUrl = Guard.Against.NullOrWhiteSpace(originalSnapUrl, nameof(originalSnapUrl));
        OriginalFaceUrl = Guard.Against.NullOrWhiteSpace(originalFaceUrl, nameof(originalFaceUrl));
        RecognitionFaceOssUrl =
            Guard.Against.NullOrWhiteSpace(recognitionFaceOssUrl, nameof(recognitionFaceOssUrl));

        // 触发图片同步事件
        AddEvent(new DomainEventRecord(new SynchronizationImgEvent
        {
            EventIdentifier = EventIdentifier,
            OriginalSnapUrl = OriginalSnapUrl,
            OriginalFaceUrl = OriginalFaceUrl,
            RecognitionFaceOssUrl = RecognitionFaceOssUrl
        }));
    }

    /// <summary>
    ///     设置请假申请单信息
    /// </summary>
    /// <param name="leaveApplication">请假申请单</param>
    private void SetLeaveApplication(LeaveApplication? leaveApplication)
    {
        LeaveStatus = leaveApplication == null
            ? LeaveStatusSmartEnum.WithoutLeave
            : leaveApplication.ApplicationStatus.LeaveStatus;
        LeaveApplicationId = leaveApplication?.Id;
        ExpectedReturnTime = leaveApplication?.EndTime;
        if (ExpectedReturnTime != null && AccessType == AccessTypeSmartEnum.Leave)
        {
            AddEvent(new DomainEventRecord(new ExpectedReturnTimeDelayEvent(EventIdentifier, ExpectedReturnTime.Value)));
        }
    }

    #region 图片完整地址处理

    /// <summary>
    ///     获取完整人脸图片地址
    /// </summary>
    /// <param name="config">配置信息</param>
    /// <returns>完整人脸图片地址</returns>
    public string? GetFullFaceImgPath(IntelligentMonitorSystemHttpClientConfig? config)
    {
        if (string.IsNullOrWhiteSpace(FaceImgPath))
        {
            return null;
        }

        return config?.GetTeamExternalHost("FileServer") + "/" + FaceImgPath;
    }

    /// <summary>
    ///     获取完整抓拍图片路径
    /// </summary>
    /// <param name="config">配置信息</param>
    /// <returns>完整人脸图片地址</returns>
    public string? GetFullSnapImgPath(IntelligentMonitorSystemHttpClientConfig? config)
    {
        if (string.IsNullOrWhiteSpace(SnapImgPath))
        {
            return null;
        }

        return config?.GetTeamExternalHost("FileServer") + "/" + SnapImgPath;
    }

    /// <summary>
    ///     获取完整视频回放地址
    /// </summary>
    /// <param name="config">配置信息</param>
    /// <returns>完整视频地址</returns>
    public string? GetFullVideoPlayback(IntelligentMonitorSystemHttpClientConfig? config)
    {
        if (string.IsNullOrWhiteSpace(DevicePlayback))
        {
            return null;
        }

        return config?.GetTeamExternalHost("FileServer") + "/" + DevicePlayback;
    }

    #endregion
}