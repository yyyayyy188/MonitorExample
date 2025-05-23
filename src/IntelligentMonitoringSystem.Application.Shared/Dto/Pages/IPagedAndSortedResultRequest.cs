namespace IntelligentMonitoringSystem.Application.Shared.Dto.Pages;

/// <summary>
///     This interface is defined to standardize to request a paged and sorted result.
/// </summary>
public interface IPagedAndSortedResultRequest : IPagedResultRequest, ISortedResultRequest
{
}