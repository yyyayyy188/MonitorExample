using Dapper;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Infrastructure.Dapper.DbContexts;

namespace IntelligentMonitoringSystem.Infrastructure.Dapper.Repositories;

/// <summary>
///     ReadOnlyBasicRepository
/// </summary>
/// <param name="connectionFactory">connectionFactory</param>
/// <typeparam name="TDbContext">TDbContext</typeparam>
/// <typeparam name="TEntity">TEntity</typeparam>
public class DapperReadOnlyBasicRepository<TDbContext, TEntity>(IConnectionFactory<TDbContext> connectionFactory)
    : IReadOnlyBasicRepository<TEntity>
    where TEntity : class, IEntity
    where TDbContext : class, IDapperDbContext
{
    /// <summary>
    ///     Execute a query asynchronously using Task.
    /// </summary>
    /// <param name="sql">sql</param>
    /// <param name="param">parameters</param>
    /// <returns>IEnumerable</returns>
    public async Task<IEnumerable<TEntity>> QueryAsync(string sql, object? param = null)
    {
        using var dbConnection = connectionFactory.GetConnection();
        return await dbConnection.QueryAsync<TEntity>(sql, param);
    }

    /// <summary>
    ///     Execute a single-row query asynchronously using Task.
    /// </summary>
    /// <param name="sql">sql</param>
    /// <param name="param">parameters</param>
    /// <returns>TEntity</returns>
    public async Task<TEntity?> QueryFirstOrDefaultAsync(string sql, object? param = null)
    {
        using var dbConnection = connectionFactory.GetConnection();
        return await dbConnection.QueryFirstOrDefaultAsync<TEntity>(sql, param);
    }
}