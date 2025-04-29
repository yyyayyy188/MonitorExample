namespace IntelligentMonitoringSystem.Domain.Shared.Configs;

/// <summary>
///     接口配置实体
/// </summary>
public class ApiEntity
{
    /// <summary>
    ///     请求头
    /// </summary>
    public Dictionary<string, string> Headers { get; set; }

    /// <summary>
    ///     请求参数
    /// </summary>
    public Dictionary<string, string> QueryParams { get; set; }

    /// <summary>
    ///     请求域名
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    ///     请求地址，域名后面的东西
    /// </summary>
    public string Address { get; set; }
}