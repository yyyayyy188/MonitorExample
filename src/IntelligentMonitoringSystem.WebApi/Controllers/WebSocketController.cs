using System.Net.WebSockets;
using IntelligentMonitoringSystem.Application.Shared.Dto;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Events;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using IntelligentMonitoringSystem.WebApi.Extensions;
using IntelligentMonitoringSystem.WebApi.WebSockets;
using Microsoft.AspNetCore.Mvc;

namespace IntelligentMonitoringSystem.WebApi.Controllers;

/// <summary>
///     WebSocketController.
/// </summary>
[ApiController]
[CleanSvcRoute]
public class WebSocketController(
    ILogger<WebSocketController> logger,
    WebSocketHandler webSocketHandler,
    IDomainEventsDispatcher domainEventsDispatcher) : ControllerBase
{
    /// <summary>
    ///     获取WebSocket连接.
    /// </summary>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    [HttpGet("web-socket")]
    public async Task WebSocket()
    {
        if (!HttpContext.WebSockets.IsWebSocketRequest)
        {
            return;
        }

        using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
        try
        {
            await webSocketHandler.OnConnected(webSocket);
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!receiveResult.CloseStatus.HasValue)
            {
                if (webSocket.State == WebSocketState.Open)
                {
                    receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                }
                else if (webSocket.State == WebSocketState.Aborted || webSocket.State == WebSocketState.Closed ||
                         webSocket.State == WebSocketState.CloseReceived || webSocket.State == WebSocketState.CloseSent)
                {
                    logger.LogWarning("webSocket 连接已经关闭。");
                    break;
                }
            }

            await webSocketHandler.OnDisconnected(webSocket);
        }
        catch (WebSocketException webSocketException) when (webSocketException.WebSocketErrorCode ==
                                                            WebSocketError.ConnectionClosedPrematurely)
#pragma warning disable S108
        {
        }
#pragma warning restore S108
        finally
        {
            await webSocketHandler.OnDisconnected(webSocket);
        }
    }

    /// <summary>
    ///     WebSocket 相应格式.
    /// </summary>
    /// <param name="eventIdentifier">事件Id.</param>
    /// <param name="accessType">设备类型 Enter|Leave.</param>
    /// <returns>ResponseEntity{AccessRecordNotificationResponseDto}.</returns>
    [HttpGet("web-socket/{accessType}/{eventIdentifier:guid}")]
    public async Task<ResponseEntity<bool>> Message(
        [FromRoute] Guid eventIdentifier, [FromRoute] string accessType)
    {
        await domainEventsDispatcher.DispatchImmediateEvents(new List<WebSocketNotificationEvent>
        {
            new(
                eventIdentifier,
                AccessTypeSmartEnum.FromName(accessType))
        });
        return ResponseEntity<bool>.Ok(true);
    }

    /// <summary>
    ///     延迟消息测试.
    /// </summary>
    /// <param name="eventIdentifier">事件Id.</param>
    /// <param name="delay">延迟秒数</param>
    /// <returns>ResponseEntity{AccessRecordNotificationResponseDto}.</returns>
    [HttpGet("web-socket/{eventIdentifier:guid}")]
    public async Task<ResponseEntity<bool>> DelayMessage(
        [FromRoute] Guid eventIdentifier, [FromQuery] int delay)
    {
        await domainEventsDispatcher.DispatchImmediateEvents(new List<IDomainEvent>
        {
            new ExpectedReturnTimeDelayEvent
            {
                EventIdentifier = eventIdentifier, ExpectedReturnTime = DateTime.Now.AddSeconds(delay)
            }
        });
        return ResponseEntity<bool>.Ok(true);
    }
}