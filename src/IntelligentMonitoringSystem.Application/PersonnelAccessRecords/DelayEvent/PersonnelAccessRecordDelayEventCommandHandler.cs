using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Configs;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Const;
using Microsoft.Extensions.Options;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.DelayEvent;

/// <summary>
///     延迟事件
/// </summary>
/// <param name="optionsMonitor">optionsMonitor</param>
/// ///
/// <param name="abnormalPersonnelAccessRecordRepository">abnormalPersonnelAccessRecordRepository</param>
public class PersonnelAccessRecordDelayEventCommandHandler(
    IOptionsMonitor<PersonnelAccessRecordConfig>? optionsMonitor,
    IAbnormalPersonnelAccessRecordRepository abnormalPersonnelAccessRecordRepository)
    : ICommandHandler<PersonnelAccessRecordDelayEventCommand>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task Handle(PersonnelAccessRecordDelayEventCommand request, CancellationToken cancellationToken)
    {
        var currentDateTime = DateTime.Now;
        await abnormalPersonnelAccessRecordRepository.SaveNotReturnOnTimeRecordAsync(
            [request.EventIdentifier],
            Convert.ToDateTime(currentDateTime.ToString(PersonnelAccessRecordConst.FormatWaringDateTime))
                .AddSeconds(optionsMonitor?.CurrentValue.WaringIntervalSecond ?? 7200));
    }
}