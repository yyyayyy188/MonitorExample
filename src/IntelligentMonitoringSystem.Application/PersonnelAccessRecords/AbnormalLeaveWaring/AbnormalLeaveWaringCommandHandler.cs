using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Domain.MessageCenters;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.MessageCenters.Enums;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Configs;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Const;
using Microsoft.Extensions.Options;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.AbnormalLeaveWaring;

/// <summary>
///     異常離開警報
/// </summary>
public class AbnormalLeaveWaringCommandHandler(
    IAbnormalPersonnelAccessRecordRepository abnormalPersonnelAccessRecordRepository,
    IMessageCenterManage messageCenterManage,
    IOptionsMonitor<PersonnelAccessRecordConfig> optionsMonitor)
    : ICommandHandler<AbnormalLeaveWaringCommand>
{
    /// <summary>
    ///     異常離開警報
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Task</returns>
    public async Task Handle(AbnormalLeaveWaringCommand request, CancellationToken cancellationToken)
    {
        var currentDateTime = DateTime.Now;
        var abnormalPersonnelAccessRecords =
            await abnormalPersonnelAccessRecordRepository.QueryWaringRecordAsync(
                Convert.ToDateTime(currentDateTime.ToString(PersonnelAccessRecordConst.FormatWaringDateTime)));
        if (abnormalPersonnelAccessRecords == null || abnormalPersonnelAccessRecords.Count == 0)
        {
            return;
        }

        foreach (var abnormalPersonnelAccessRecord in abnormalPersonnelAccessRecords)
        {
            await messageCenterManage.PushAsync(new EventMessage
            {
                EventSource = EventSourceSmartEnum.AbnormalLeave,
                EventSourceData = new Dictionary<string, object>
                {
                    ["PersonnelAccessRecordId"] = abnormalPersonnelAccessRecord.PersonnelAccessRecord.Id
                }
            });
        }

        await abnormalPersonnelAccessRecordRepository.UpdateAbnormalPersonnelAccessRecordNextWaring(
            abnormalPersonnelAccessRecords.Select(x => x.Id).ToList(), Convert
                .ToDateTime(DateTime.Now.ToString(PersonnelAccessRecordConst.FormatWaringDateTime))
                .AddSeconds(optionsMonitor?.CurrentValue.WaringIntervalSecond ?? 7200));
    }
}