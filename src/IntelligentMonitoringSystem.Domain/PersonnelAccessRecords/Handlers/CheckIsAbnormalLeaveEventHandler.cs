using IntelligentMonitoringSystem.Domain.MessageCenters;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Events;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.OrderBys;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.MessageCenters.Enums;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using IntelligentMonitoringSystem.Domain.Users;
using MediatR;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Handlers;

/// <summary>
///     检测是否为异常离场事件处理器
/// </summary>
public class CheckIsAbnormalLeaveEventHandler(
    IBasicRepository<AbnormalPersonnelAccessRecord> basicRepository,
    IMessageCenterManage messageCenterManage,
    IUserRepository userRepository,
    IAbnormalPersonnelAccessRecordRepository abnormalPersonnelAccessRecordRepository)
    : INotificationHandler<CheckIsAbnormalLeaveEvent>
{
    /// <summary>
    ///     检测是否为异常离场事件处理器
    /// </summary>
    /// <param name="notification">notification.</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task Handle(CheckIsAbnormalLeaveEvent notification, CancellationToken cancellationToken)
    {
        var outStatus = AbnormalPersonnelAccessRecordStatusSmartEnum.Out;
        var abnormalPersonnelAccessRecord = await basicRepository.FirstOrDefaultAsync(
            x => x.FaceId == notification.FaceId && x.Status == outStatus,
            new AbnormalPersonnelAccessRecordCreateTimeOrderBy());
        if (abnormalPersonnelAccessRecord == null)
        {
            return;
        }

        abnormalPersonnelAccessRecord.ReturnTime = notification.AccessTime;
        abnormalPersonnelAccessRecord.Status = AbnormalPersonnelAccessRecordStatusSmartEnum.In;
        abnormalPersonnelAccessRecord.LastEditTime = DateTime.Now;
        await basicRepository.UpdateAsync(abnormalPersonnelAccessRecord, cancellationToken);

        // 平均时长
        var averageTime = await abnormalPersonnelAccessRecordRepository.GetAbnormalAccessAverageTimeAsync(notification.FaceId);
        if (averageTime > 0)
        {
            await userRepository.SynchronizationAbnormalAccessAverageTimeAsync(notification.FaceId, averageTime);
            await userRepository.SynchronizationAbnormalLeaveAccessInfoAsync(notification.FaceId);
        }

        await messageCenterManage.PushAsync(new EventMessage
        {
            EventSource = EventSourceSmartEnum.AbnormalEnter,
            EventSourceData =
                new Dictionary<string, object> { ["EventIdentifier"] = abnormalPersonnelAccessRecord.EventIdentifier }
        });
    }
}