using System.Net.WebSockets;
using System.Text;
using IntelligentMonitoringSystem.WebApi.WebSockets.Manages;

namespace IntelligentMonitoringSystem.WebApi.WebSockets;

/// <summary>
///     WebSocketHandler
/// </summary>
/// <param name="webSocketConnectionManager">webSocketConnectionManager</param>
public class WebSocketHandler(ConnectionManager webSocketConnectionManager)
{
    /// <summary>
    ///     是否存在连接
    /// </summary>
    /// <returns>bool</returns>
    public virtual bool IsAvailable()
    {
        return webSocketConnectionManager.IsAvailable();
    }

    /// <summary>
    ///     连接建立
    /// </summary>
    /// <param name="socket">socket</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public virtual Task OnConnected(WebSocket socket)
    {
        webSocketConnectionManager.AddSocket(socket);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     连接断开
    /// </summary>
    /// <param name="socket">socket</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public virtual async Task OnDisconnected(WebSocket socket)
    {
        await webSocketConnectionManager.RemoveSocket(webSocketConnectionManager.GetId(socket));
    }

    /// <summary>
    ///     发送消息
    /// </summary>
    /// <param name="socket">socket</param>
    /// <param name="message">message</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public virtual async Task SendMessageAsync(WebSocket socket, string message)
    {
        if (socket.State != WebSocketState.Open)
        {
            await webSocketConnectionManager.RemoveSocket(webSocketConnectionManager.GetId(socket));
            return;
        }

        var messageByte = Encoding.UTF8.GetBytes(message);
        await socket.SendAsync(
            new ArraySegment<byte>(
                messageByte,
                0,
                messageByte.Length),
            WebSocketMessageType.Text,
            true,
            CancellationToken.None);
    }

    /// <summary>
    ///     发送消息
    /// </summary>
    /// <param name="socketId">socketId</param>
    /// <param name="message">message</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task SendMessageAsync(string socketId, string message)
    {
        await webSocketConnectionManager.RemoveSocket(socketId);
        await SendMessageAsync(webSocketConnectionManager.GetSocketById(socketId), message);
    }

    /// <summary>
    ///     发送消息给所有连接
    /// </summary>
    /// <param name="message">message</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task SendMessageToAllAsync(string message)
    {
        // 清理已关闭连接
        webSocketConnectionManager.ClearClosedSocket();

        var openSocket = webSocketConnectionManager.QueryOpenSocket();
        if (!openSocket.Any())
        {
            return;
        }

        var taskList = openSocket.Select(x => x.Value).Select(webSocket => SendMessageAsync(webSocket, message))
            .ToList();

        await Task.WhenAll(taskList);
    }
}