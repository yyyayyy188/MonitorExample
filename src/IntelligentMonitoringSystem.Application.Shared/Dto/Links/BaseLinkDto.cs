using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.Shared.Dto.Links;

/// <summary>
///     Base Link
/// </summary>
public class BaseLinkDto
{
    /// <summary>
    ///     Link
    /// </summary>
    [JsonProperty("link")]
    public LinkDto? Link { get; private set; }

    /// <summary>
    ///     add link
    /// </summary>
    /// <param name="rel">rel</param>
    /// <param name="href">href</param>
    /// <param name="method">method</param>
    public void AddLink(string rel, string href, HttpMethod method)
    {
        Link = LinkDto.Create(rel, href, method);
    }
}