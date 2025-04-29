using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;

namespace IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;

/// <summary>
///     Read only repository interface.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IReadOnlyBasicRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    ///     Execute a query asynchronously using Task.
    /// </summary>
    /// <param name="sql">sql</param>
    /// <param name="param">parameters</param>
    /// <returns>IEnumerable</returns>
    Task<IEnumerable<TEntity>> QueryAsync(string sql, object? param = null);

    /// <summary>
    ///     Execute a single-row query asynchronously using Task.
    /// </summary>
    /// <param name="sql">sql</param>
    /// <param name="param">parameters</param>
    /// <returns>TEntity</returns>
    Task<TEntity?> QueryFirstOrDefaultAsync(string sql, object? param = null);
}