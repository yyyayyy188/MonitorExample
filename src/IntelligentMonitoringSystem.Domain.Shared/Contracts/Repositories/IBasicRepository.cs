using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using IntelligentMonitoringSystem.Domain.Shared.Linq.OrderBy;
using Microsoft.EntityFrameworkCore.Query;

namespace IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;

/// <summary>
///     The basic repository interface.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IBasicRepository<TEntity> : IRepository where TEntity : class, IEntity
{
    /// <summary>
    ///     Gets an entity with given primary key or null if not found.
    /// </summary>
    /// <param name="id">Primary key of the entity to get</param>
    /// <returns>Entity or null</returns>
    Task<TEntity?> GetAsync<TKey>(TKey id) where TKey : notnull;

    /// <summary>
    ///     是否存在记录
    /// </summary>
    /// <param name="predicate">predicate</param>
    /// <returns>bool</returns>
    Task<bool> IsExistAsync(Expression<Func<TEntity, bool>>? predicate = null);

    /// <summary>
    ///     Gets all entities.
    /// </summary>
    /// <returns>IEnumerable</returns>
    Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    ///     Inserts a new entity.
    /// </summary>
    /// <param name="entity">Inserted entity</param>
    /// <param name="cancellationToken">
    ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to
    ///     complete.
    /// </param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task<TEntity> InsertAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Bulk inserts a new entity.
    /// </summary>
    /// <param name="entities">Inserted entities</param>
    /// <param name="cancellationToken">
    ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to
    ///     complete.
    /// </param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task<int> BulkInsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Update an entity.
    /// </summary>
    /// <param name="entity">Inserted entity</param>
    /// <param name="cancellationToken">
    ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to
    ///     complete.
    /// </param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task<bool> UpdateAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Update an entity.
    /// </summary>
    /// <param name="entity">update entity</param>
    /// <param name="action">action</param>
    /// <param name="cancellationToken">
    ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to
    ///     complete.
    /// </param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task<bool> UpdateAsync([NotNull] TEntity entity, Action<TEntity> action, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Delete a entity.
    /// </summary>
    /// <param name="entity">Inserted entity</param>
    /// <param name="cancellationToken">
    ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to
    ///     complete.
    /// </param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task<bool> DeleteAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Execute Sql
    /// </summary>
    /// <param name="sql">sql</param>
    /// <param name="parameters">parameters</param>
    /// <returns>int</returns>
    Task<int> ExecuteSqlAsync(string sql, params object[] parameters);

    /// <summary>
    ///     Execute Update
    /// </summary>
    /// <param name="predicate">predicate</param>
    /// <param name="setPropertyCalls">setPropertyCalls</param>
    /// <returns>int</returns>
    Task<int> ExecuteUpdateAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls);

    /// <summary>
    ///     Execute Delete
    /// </summary>
    /// <param name="predicate">predicate</param>
    /// <returns>int</returns>
    Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    ///     FirstOrDefault
    /// </summary>
    /// <param name="predicate">predicate</param>
    /// <param name="orderBy">orderBy</param>
    /// <param name="selector">selector</param>
    /// <returns>TEntity?</returns>
    Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        IOrderBy<TEntity, dynamic>? orderBy = null,
        Expression<Func<TEntity, TEntity>>? selector = null);

    /// <summary>
    ///     Query
    /// </summary>
    /// <param name="predicate">predicate</param>
    /// <param name="selector">selector</param>
    /// <returns>List<TEntity /></returns>
    Task<IEnumerable<TEntity>> QueryAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Expression<Func<TEntity, TEntity>>? selector = null);

    /// <summary>
    ///     分页查询
    /// </summary>
    /// <param name="pageSize">页大小</param>
    /// <param name="skipCount">跳过数量</param>
    /// <param name="predicate">筛选条件</param>
    /// <param name="orderBy">排序方式</param>
    /// <param name="selector">选择器</param>
    /// <returns>IEnumerable<TEntity /></returns>
    Task<List<TEntity>> PageAsync(
        int pageSize,
        int skipCount,
        Expression<Func<TEntity, bool>>? predicate = null,
        IOrderBy<TEntity, dynamic>? orderBy = null,
        Expression<Func<TEntity, TEntity>>? selector = null);

    /// <summary>
    ///     数量统计
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns>Task<int /></returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);
}