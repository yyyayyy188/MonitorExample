using IntelligentMonitoringSystem.Application.Shared.Dto.Lists;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.Shared.Dto.Pages;

/// <summary>
///     Implements <see cref="IPagedResult{T}" />.
/// </summary>
/// <typeparam name="T">Type of the items in the <see cref="ListResultDto{T}.Items" /> list</typeparam>
[Serializable]
public class PagedResultDto<T> : ListResultDto<T>, IPagedResult<T>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PagedResultDto{T}" /> class.
    ///     Creates a new <see cref="PagedResultDto{T}" /> object.
    /// </summary>
    public PagedResultDto()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PagedResultDto{T}" /> class.
    ///     Creates a new <see cref="PagedResultDto{T}" /> object.
    /// </summary>
    /// <param name="totalCount">Total count of Items</param>
    /// <param name="items">List of items in current page</param>
    public PagedResultDto(long totalCount, IReadOnlyList<T> items)
        : base(items)
    {
        TotalCount = totalCount;
    }

    /// <inheritdoc />
    [JsonProperty("total")]
    public long TotalCount { get; set; }
}