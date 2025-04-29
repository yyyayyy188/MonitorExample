using IntelligentMonitoringSystem.Application.Shared.Dto.Lists;

namespace IntelligentMonitoringSystem.Application.Shared.Dto.Pages;

/// <summary>
///     This interface is defined to standardize to request a paged result.
/// </summary>
public interface IPagedResultRequest : ILimitedResultRequest
{
    /// <summary>
    ///     Skip count (beginning of the page).
    /// </summary>
    int SkipCount { get; set; }
}