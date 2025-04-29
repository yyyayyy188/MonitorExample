using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Const;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentMonitoringSystem.Infrastructure.Pulsar.PersonnelAccessRecords.Repositories;

/// <summary>
///     考勤记录延迟事件
/// </summary>
public class PersonnelAccessRecordDelayRepository(
    [FromKeyedServices(PersonnelAccessRecordConst.TopicName)]
    IProducer<string> producer)
    : IPersonnelAccessRecordDelayRepository
{
    /// <summary>
    ///     延迟事件
    /// </summary>
    /// <param name="eventIdentifier">事件标识.</param>
    /// <param name="eventTime">事件事件.</param>
    /// <returns>Task.</returns>
    public async Task PushDelayEventAsync(string eventIdentifier, DateTime eventTime)
    {
        await producer.NewMessage().DeliverAt(eventTime).Send(eventIdentifier);
    }
}