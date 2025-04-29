using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.Shared.Dto.Links;

/// <summary>
///     Lint Dto
/// </summary>
public class LinkDto
{
    /// <summary>
    ///     Rel
    /// </summary>
    [JsonProperty("rel")]
    public string? Rel { get; private set; }

    /// <summary>
    ///     Href
    /// </summary>
    [JsonProperty("href")]
    public string? Href { get; private set; }

    /// <summary>
    ///     Action
    /// </summary>
    [JsonProperty("action")]
    public string? Action { get; private set; }

    /// <summary>
    ///     Create
    /// </summary>
    /// <param name="rel">rel</param>
    /// <param name="href">href</param>
    /// <param name="method">method</param>
    /// <returns>LinkDto</returns>
    public static LinkDto Create(string rel, string href, HttpMethod method)
    {
        return new LinkDto { Rel = rel, Href = href, Action = method.ToString() };
    }
}