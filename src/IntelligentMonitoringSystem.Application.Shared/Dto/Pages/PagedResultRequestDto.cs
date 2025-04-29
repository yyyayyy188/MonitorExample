using IntelligentMonitoringSystem.Application.Shared.Dto.Lists;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.Shared.Dto.Pages;

/// <summary>
///     Simply implements <see cref="IPagedResultRequest" />.
/// </summary>
[Serializable]
public class PagedResultRequestDto : LimitedResultRequestDto, IPagedResultRequest
{
    /// <summary>
    ///     skipCount
    /// </summary>
    [JsonProperty("skipCount")]
    public virtual int SkipCount { get; set; }
}