using System.Data;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;

namespace IntelligentMonitoringSystem.Domain.Shared.Contracts.UnitOfWorks;

/// <summary>
///     UnitOfWork Interface
/// </summary>
public interface IUnitOfWorkAsync : IAsyncDisposable
{
    /// <summary>
    ///     Gets the custom repository for the <typeparamref name="TRepository" />.
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    /// <returns>TRepository</returns>
    Task<TRepository> GetCustomRepositoryAsync<TRepository>() where TRepository : class, IRepository;

    /// <summary>
    ///     Gets the specified repository for the <typeparamref name="TEntity" />.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns>IBasicRepository</returns>
    Task<IBasicRepository<TEntity>> GetBasicRepositoryAsync<TEntity>() where TEntity : class, IEntity;

    /// <summary>
    ///     Asynchronously commit all changes made in this unit of work to the database.
    /// </summary>
    /// <param name="action">action</param>
    /// <returns>TRepository</returns>
    Task<bool> CommitAsync(Func<Task> action);

    /// <summary>
    ///     Asynchronously begin transaction to the database.
    /// </summary>
    /// <param name="isolationLevel">isolationLevel</param>
    /// <returns>Task</returns>
    Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.RepeatableRead);
}