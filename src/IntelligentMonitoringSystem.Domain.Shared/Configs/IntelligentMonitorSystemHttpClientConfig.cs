namespace IntelligentMonitoringSystem.Domain.Shared.Configs;

/// <summary>
///     HttpClient配置
/// </summary>
public class IntelligentMonitorSystemHttpClientConfig
{
    /// <summary>
    ///     位置
    /// </summary>
    public const string PositionOptions = "HttpApiConfig";

    /// <summary>
    ///     Gets or sets 团队Api配置
    /// </summary>
    public Dictionary<string, TeamConfigEntity> TeamApiConfigList { get; set; } =
        new();

    /// <summary>
    ///     获取团队主机外部
    /// </summary>
    /// <param name="teamName">teamName.</param>
    /// <returns>string.</returns>
    /// <exception cref="ArgumentNullException">ArgumentNullException.</exception>
    /// <exception cref="ArgumentException">ArgumentException.</exception>
    public string GetTeamExternalHost(string teamName)
    {
        if (string.IsNullOrWhiteSpace(teamName))
        {
            throw new ArgumentNullException(nameof(teamName), "teamName is null or empty");
        }

        if (!TeamApiConfigList.TryGetValue(teamName, out var teamConfig))
        {
            throw new ArgumentException(
                $"Find team api config failure for {nameof(teamName)}");
        }

        return teamConfig.ExternalHost;
    }

    /// <summary>
    ///     获取团队主机
    /// </summary>
    /// <param name="teamName">teamName.</param>
    /// <returns>string.</returns>
    /// <exception cref="ArgumentNullException">ArgumentNullException.</exception>
    /// <exception cref="ArgumentException">ArgumentException.</exception>
    public string GetTeamHost(string teamName)
    {
        if (string.IsNullOrWhiteSpace(teamName))
        {
            throw new ArgumentNullException(nameof(teamName), "teamName is null or empty");
        }

        if (!TeamApiConfigList.TryGetValue(teamName, out var teamConfig))
        {
            throw new ArgumentException(
                $"Find team api config failure for {nameof(teamName)}");
        }

        return teamConfig.Host;
    }

    /// <summary>
    ///     获取团队Api配置
    /// </summary>
    /// <param name="teamName">团队名称.</param>
    /// <param name="apiName">api名称.</param>
    /// <returns>ApiEntity.</returns>
    public ApiEntity GetApiEntity(string teamName, string apiName)
    {
        if (string.IsNullOrWhiteSpace(apiName))
        {
            throw new ArgumentNullException(nameof(apiName), "apiName is null or empty");
        }

        if (string.IsNullOrWhiteSpace(teamName))
        {
            throw new ArgumentNullException(nameof(teamName), "teamName is null or empty");
        }

        if (!TeamApiConfigList.TryGetValue(teamName, out var teamConfig))
        {
            throw new ArgumentException(
                $"Find team api config failure for {nameof(teamName)}");
        }

        if (!teamConfig.ApiConfigList.TryGetValue(apiName, out var apiEntity))
        {
            throw new ArgumentException(
                $"Find  api config failure for {nameof(apiName)}");
        }

        if (string.IsNullOrWhiteSpace(apiEntity.Host))
        {
            apiEntity.Host = teamConfig.Host;
        }

        return apiEntity;
    }
}