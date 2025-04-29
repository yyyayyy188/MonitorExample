using IntelligentMonitoringSystem.Application.Shared.Dto.Lists;

namespace IntelligentMonitoringSystem.Application.Shared.Dto.Pages;

/// <summary>
///     This interface is defined to standardize to return a page of items to clients.
/// </summary>
/// <typeparam name="T">Type of the items in the <see cref="IListResult{T}.Items" /> list</typeparam>
public interface IPagedResult<T> : IListResult<T>, IHasTotalCount
{
}