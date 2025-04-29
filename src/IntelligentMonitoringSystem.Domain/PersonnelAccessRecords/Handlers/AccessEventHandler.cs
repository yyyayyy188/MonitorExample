using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Events;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Configs;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using MediatR;
using Microsoft.Extensions.Options;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Handlers;

/// <summary>
///     考勤事件处理器
/// </summary>
public class AccessEventHandler(
    IBasicRepository<AbnormalPersonnelAccessRecord> basicRepository,
    IOptionsMonitor<PersonnelAccessRecordConfig> optionsMonitor)
    : INotificationHandler<AccessEvent>
{
    /// <summary>
    ///     考勤事件处理器
    /// </summary>
    /// <param name="notification">notification</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Task</returns>
    public async Task Handle(AccessEvent notification, CancellationToken cancellationToken)
    {
        // 如果为第一次设置异常则需要初始化异常记录
        if ((notification.OriginalAccessStatus == AccessStatusSmartEnum.Pending ||
             notification.OriginalAccessStatus == AccessStatusSmartEnum.Normal) &&
            notification.AccessStatus == AccessStatusSmartEnum.Abnormal)
        {
            await basicRepository.InsertAsync(
                new AbnormalPersonnelAccessRecord(
                    notification.PersonnelAccessRecordId,
                    notification.AccessTime,
                    optionsMonitor?.CurrentValue,
                    notification.FaceId), cancellationToken);
        }

        if (notification.OriginalAccessStatus == AccessStatusSmartEnum.Abnormal &&
            notification.AccessStatus == AccessStatusSmartEnum.Normal)
        {
            await basicRepository.ExecuteDeleteAsync(x =>
                x.PersonnelAccessRecordId == notification.PersonnelAccessRecordId);
        }
    }
}