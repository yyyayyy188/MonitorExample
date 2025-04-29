using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Infrastructure.Reference.Apis.FileServers.Models;

/// <summary>
///     文件上传返回
/// </summary>
public class FileServerUploadResponse
{
    /// <summary>
    ///     文件名称
    /// </summary>
    [JsonProperty("Name")]
    public string Name { get; set; }

    /// <summary>
    ///     文件路径
    /// </summary>
    [JsonProperty("Path")]
    public string Path { get; set; }
}