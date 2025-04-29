namespace IntelligentMonitoringSystem.Application.Shared.Dto.Pages;

/// <summary>
///     Simply implements <see cref="IPagedAndSortedResultRequest" />.
/// </summary>
[Serializable]
public class PagedAndSortedResultRequestDto : PagedResultRequestDto, IPagedAndSortedResultRequest
{
    /// <summary>
    ///     Sorting
    /// </summary>
    public virtual string? Sorting { get; set; }
}