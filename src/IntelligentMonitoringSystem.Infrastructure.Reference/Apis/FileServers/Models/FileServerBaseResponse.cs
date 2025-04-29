using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Infrastructure.Reference.Apis.FileServers.Models;

/// <summary>
///     文件服务器通用响应
/// </summary>
/// <typeparam name="T">T.</typeparam>
public class FileServerBaseResponse<T>
{
    /// <summary>
    ///     状态码
    /// </summary>
    [JsonProperty("code")]
    public int Code { get; set; }

    /// <summary>
    ///     状态描述
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }

    /// <summary>
    ///     数据
    /// </summary>
    [JsonProperty("data")]
    public T? Data { get; set; }
}