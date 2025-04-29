using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace IntelligentMonitoringSystem.WebApi.WebSockets.Manages;

/// <summary>
///     连接管理器
/// </summary>
public class ConnectionManager
{
    /// <summary>
    ///     断开连接
    /// </summary>
    private readonly static List<WebSocketState> NoConnectedStatusList =
        [WebSocketState.None, WebSocketState.Aborted, WebSocketState.Connecting, WebSocketState.Closed];

    private readonly ConcurrentDictionary<string, WebSocket> sockets = new();

    /// <summary>
    ///     心跳
    /// </summary>
    /// <returns>bool</returns>
    public bool IsAvailable()
    {
        return sockets?.Any(x => x.Value.State == WebSocketState.Open) ?? false;
    }

    /// <summary>
    ///     根据id获取连接
    /// </summary>
    /// <param name="id">id.</param>
    /// <returns>WebSocket</returns>
    public WebSocket GetSocketById(string id)
    {
        return sockets.FirstOrDefault(p => p.Key == id).Value;
    }

    /// <summary>
    ///     获取所有连接
    /// </summary>
    /// <returns>ConcurrentDictionary</returns>
    public List<KeyValuePair<string, WebSocket>> QueryOpenSocket()
    {
        return sockets.Where(x => x.Value.State == WebSocketState.Open).ToList();
    }

    /// <summary>
    ///     根据连接获取id
    /// </summary>
    /// <param name="socket">socket</param>
    public void AddSocket(WebSocket socket)
    {
        sockets.TryAdd(CreateConnectionId(), socket);
    }

    /// <summary>
    ///     清理关闭的连接
    /// </summary>
    public void ClearClosedSocket()
    {
        var keyValuePairs = sockets.Where(x => x.Value.State != WebSocketState.Open).ToList();
        foreach (var item in keyValuePairs)
        {
            sockets.TryRemove(item.Key, out _);
        }
    }

    /// <summary>
    ///     移除连接
    /// </summary>
    /// <param name="id">id.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task RemoveSocket(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return;
        }

        if (sockets.TryRemove(id, out var socket))
        {
            if (NoConnectedStatusList.Contains(socket.State))
            {
                return;
            }

            await socket.CloseAsync(
                WebSocketCloseStatus.NormalClosure,
                "Closed by the ConnectionManager",
                CancellationToken.None);
        }
    }

    /// <summary>
    ///     根据socket获取
    /// </summary>
    /// <param name="socket">socket</param>
    /// <returns>string</returns>
    public string GetId(WebSocket socket)
    {
        var keyValuePair = sockets.FirstOrDefault(p => p.Value == socket);
        return keyValuePair.Key;
    }

    /// <summary>
    ///     生成连接Id
    /// </summary>
    /// <returns>String.</returns>
    private static string CreateConnectionId()
    {
        return Guid.NewGuid().ToString();
    }
}