using System.Buffers;
using System.Text;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.DelayEvent;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Const;
using MediatR;

namespace IntelligentMonitoringSystem.WebApi.BackgroundServices;

/// <summary>
///     延迟消息处理
/// </summary>
/// <param name="logger">logger</param>
/// <param name="pulsarClient">pulsarClient</param>
/// <param name="serviceScopeFactory">serviceScopeFactory</param>
public class PersonnelAccessRecordDelayMessageBackgroundService(
    ILogger<PersonnelAccessRecordDelayMessageBackgroundService> logger,
    IPulsarClient pulsarClient,
    IServiceScopeFactory serviceScopeFactory)
    : BackgroundService
{
    /// <summary>
    ///     Execute
    /// </summary>
    /// <param name="stoppingToken">stoppingToken</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await using var pulsarConsumer = pulsarClient.NewConsumer()
                .SubscriptionName(PersonnelAccessRecordConst.SubscriptionName)
                .Topic(PersonnelAccessRecordConst.TopicName)
                .SubscriptionType(SubscriptionType.Shared)
                .Create();

            await foreach (var message in pulsarConsumer.Messages(stoppingToken))
            {
                var messageBody = Encoding.UTF8.GetString(message.Data.ToArray());
                if (!string.IsNullOrWhiteSpace(messageBody))
                {
                    try
                    {
                        if (Guid.TryParse(messageBody, out var eventIdentifier))
                        {
                            using var scope = serviceScopeFactory.CreateScope();
                            var sender = scope.ServiceProvider.GetService<ISender>();

                            await sender.Send(
                                new PersonnelAccessRecordDelayEventCommand { EventIdentifier = eventIdentifier }, stoppingToken);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Error processing message, {ex.Message}");
                    }
                }

                await pulsarConsumer.Acknowledge(message, stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}