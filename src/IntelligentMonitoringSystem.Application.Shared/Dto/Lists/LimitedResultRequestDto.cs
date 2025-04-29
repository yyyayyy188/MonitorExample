using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.Shared.Dto.Lists;

/// <summary>
///     Simply implements <see cref="ILimitedResultRequest" />.
/// </summary>
[Serializable]
public class LimitedResultRequestDto : ILimitedResultRequest
{
    /// <summary>
    ///     Default value: 10.
    /// </summary>
    public static int DefaultMaxResultCount { get; set; } = 10;

    /// <summary>
    ///     Maximum result count should be returned.
    ///     This is generally used to limit result count on paging.
    /// </summary>
    [JsonProperty("maxResultCount")]
    public virtual int MaxResultCount { get; set; } = DefaultMaxResultCount;
}