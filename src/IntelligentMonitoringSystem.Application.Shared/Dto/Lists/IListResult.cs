namespace IntelligentMonitoringSystem.Application.Shared.Dto.Lists;

/// <summary>
///     This interface is defined to standardize to return a list of items to clients.
/// </summary>
/// <typeparam name="T">Type of the items in the <see cref="Items" /> list</typeparam>
public interface IListResult<T>
{
    /// <summary>
    ///     List of items.
    /// </summary>
    IReadOnlyList<T> Items { get; set; }
}