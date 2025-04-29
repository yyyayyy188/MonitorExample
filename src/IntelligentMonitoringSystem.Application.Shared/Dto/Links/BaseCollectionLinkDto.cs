using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.Shared.Dto.Links;

/// <summary>
///     Base collection link dto
/// </summary>
public class BaseCollectionLinkDto
{
    /// <summary>
    ///     Links
    /// </summary>
    [JsonProperty("links")]
    public IList<LinkDto?> Links { get; private set; } = new List<LinkDto?>();

    /// <summary>
    ///     add link
    /// </summary>
    /// <param name="rel">rel</param>
    /// <param name="href">href</param>
    /// <param name="method">method</param>
    public void AddLink(string rel, string href, HttpMethod method)
    {
        Links.Add(LinkDto.Create(rel, href, method));
    }

    /// <summary>
    ///     add links
    /// </summary>
    /// <param name="link">link</param>
    public void AddLinks(LinkDto link)
    {
        Links.Add(link);
    }

    /// <summary>
    ///     clear link
    /// </summary>
    public void ClearLink()
    {
        Links.Clear();
    }
}