using IntelligentMonitoringSystem.Application.MessageCenters.WebSocket;
using IntelligentMonitoringSystem.Domain.MessageCenters;
using IntelligentMonitoringSystem.WebApi.WebSockets;
using MediatR;

namespace IntelligentMonitoringSystem.WebApi.BackgroundServices;

/// <summary>
///     消息中心后台服务
/// </summary>
/// <param name="serviceScopeFactory">serviceScopeFactory</param>
/// <param name="messageCenterManage">messageCenterManage</param>
/// <param name="webSocketHandler">webSocketHandler</param>
/// <param name="logger">logger</param>
public class MessageCenterBackGroundService(
    IServiceScopeFactory serviceScopeFactory,
    IMessageCenterManage messageCenterManage,
    WebSocketHandler webSocketHandler,
    ILogger<MessageCenterBackGroundService> logger) : BackgroundService
{
    /// <summary>
    ///     启动
    /// </summary>
    /// <param name="stoppingToken">stoppingToken</param>
    /// <returns>Task</returns>
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var message = await messageCenterManage.PullAsync();
                if (message == null || !webSocketHandler.IsAvailable())
                {
                    continue;
                }

                using var scope = serviceScopeFactory.CreateScope();
                var sender = scope.ServiceProvider.GetService<ISender>();
                var sendMessage = await sender.Send(
                    new WebSocketMessageQuery(message.EventSource, message.EventSourceData),
                    stoppingToken);
                if (!string.IsNullOrWhiteSpace(sendMessage))
                {
                    await webSocketHandler.SendMessageToAllAsync(sendMessage);
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Failure while processing queue {Ex}", ex.InnerException?.Message ?? ex.Message);
            }
        }

        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
    }
}