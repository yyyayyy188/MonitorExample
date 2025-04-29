namespace IntelligentMonitoringSystem.Domain.Shared.Configs;

/// <summary>
///     团队配置实体
/// </summary>
public class TeamConfigEntity
{
    /// <summary>
    ///     主机地址
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    ///     外部地址
    /// </summary>
    public string ExternalHost { get; set; }

    /// <summary>
    ///     基础Token
    /// </summary>
    public string BaseToken { get; set; }

    /// <summary>
    ///     API数据集合
    /// </summary>
    public Dictionary<string, ApiEntity> ApiConfigList { get; set; }
}