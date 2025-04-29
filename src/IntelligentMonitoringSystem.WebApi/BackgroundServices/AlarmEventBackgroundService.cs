using System.Buffers;
using System.Text;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using IntelligentMonitoringSystem.Application.AlarmEventMessages.Create;
using IntelligentMonitoringSystem.Application.AlarmEventMessages.Dtos;
using IntelligentMonitoringSystem.Domain.Shared.Extensions;
using MediatR;
using Newtonsoft.Json;
using static System.Text.RegularExpressions.Regex;

namespace IntelligentMonitoringSystem.WebApi.BackgroundServices;

/// <summary>
///     Alarm event background service
/// </summary>
/// <param name="logger">logger</param>
/// <param name="pulsarClient">pulsarClient</param>
/// <param name="serviceScopeFactory">serviceScopeFactory</param>
public class AlarmEventBackgroundService(
    ILogger<AlarmEventBackgroundService> logger,
    IPulsarClient pulsarClient,
    IServiceScopeFactory serviceScopeFactory)
    : BackgroundService
{
    /// <summary>
    ///     Topic Name
    /// </summary>
    private const string TopicName = "topic_alarm_event";

    /// <summary>
    ///     Topic Name
    /// </summary>
    private const string SubscriptionName = "IntelligentMonitoringSystemSubscription";

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
                .SubscriptionName(SubscriptionName)
                .Topic(TopicName)
                .Create();

            await foreach (var message in pulsarConsumer.Messages(stoppingToken))
            {
                var messageBody = Encoding.UTF8.GetString(message.Data.ToArray());
                if (!string.IsNullOrWhiteSpace(messageBody))
                {
                    try
                    {
                        const string pattern = @"(\\[^bfrnt\\/‘\""])";
                        messageBody = Replace(messageBody, pattern, "\\$1");
                        var alarmEvent = JsonConvert.DeserializeObject<AlarmEventMessageRequest>(messageBody);
                        logger.LogInformation($"TopicAlarmEvent messageBody:{messageBody}");
                        var alarmEventMessage = alarmEvent.MsgList.FirstOrDefault();
                        if (alarmEventMessage?.Data == null ||
                            string.IsNullOrWhiteSpace(alarmEventMessage.Data.AlarmContext))
                        {
                            logger.LogWarning("message content is empty, message is {MessageBody}", messageBody);

                            await pulsarConsumer.Acknowledge(message, stoppingToken);
                            continue;
                        }

                        var alarmContextBody =
                            JsonConvert.DeserializeObject<AlarmContextBody>(alarmEventMessage.Data.AlarmContext
                                .Substring(1, alarmEventMessage.Data.AlarmContext.Length - 2)
                                .Replace("\\", string.Empty));

                        var alarmEventMessageCommand = new AlarmEventMessageCommand
                        {
                            AppId = alarmEvent.AppId,
                            Sig = alarmEvent.Sig,
                            MsgType = alarmEventMessage.MsgType,
                            DeviceId = alarmEventMessage.Data.DeviceId,
                            OriginalAlarmContext = alarmEventMessage.Data.AlarmContext,
                            DeviceName = alarmEventMessage.Data.DeviceName,
                            DeviceAddress = alarmEventMessage.Data.DeviceAddress,
                            RegionName = alarmEventMessage.Data.RegionName,
                            StoreName = alarmEventMessage.Data.StoreName,
                            StoreId = alarmEventMessage.Data.StoreId,
                            Region = alarmEventMessage.Data.Region,
                            AlarmContext = new AlarmContext
                            {
                                FaceId = alarmContextBody.FaceId,
                                Gender = alarmContextBody.Gender,
                                Age = alarmContextBody.Age,
                                SnapTime = alarmContextBody.SnapTime.ToDateTime(),
                                SnapUrl = alarmContextBody.SnapUrl,
                                Name = alarmContextBody.RealName,
                                FaceUrl = alarmContextBody.FaceUrl,
                                RecognitionFaceOssUrl = alarmContextBody.RecognitionFaceOssUrl,
                                FaceObjectId = alarmContextBody.FaceObjectId
                            }
                        };

                        if (long.TryParse(alarmEventMessage.TimeStamp, out var timeStamp))
                        {
                            alarmEventMessageCommand.TimeStamp = timeStamp.ToDateTime();
                        }

                        if (long.TryParse(alarmEventMessage.Data.DetectTime, out var detectTime))
                        {
                            alarmEventMessageCommand.DetectTime = detectTime.ToDateTime();
                        }

                        using var scope = serviceScopeFactory.CreateScope();
                        var sender = scope.ServiceProvider.GetService<ISender>();
                        await sender.Send(alarmEventMessageCommand, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError($"Error deserializing message, ex is {ex}, message body is {messageBody}");
                    }
                }

                await pulsarConsumer.Acknowledge(message, stoppingToken);
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}