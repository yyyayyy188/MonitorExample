using System.Data;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore.UnitOfWorks;

/// <inheritdoc />
public class EntityFrameWorkAsyncCoreUnitOfWorkAsync<TContext>
    : IUnitOfWorkAsync where TContext : BaseDbContext
{
    private readonly IDbContextProvider<TContext> _dbContextProvider;
    private readonly Dictionary<Type, object> _repositories = new();
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<EntityFrameWorkAsyncCoreUnitOfWorkAsync<TContext>> logger;
    private TContext? _context;
    private IDbContextTransaction? _contextTransaction;
    private bool _disposed;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EntityFrameWorkAsyncCoreUnitOfWorkAsync{TContext}" /> class.
    /// </summary>
    /// <param name="dbContextProvider">dbContextProvider</param>
    /// <param name="logger">logger</param>
    /// <param name="serviceProvider">serviceProvider</param>
    public EntityFrameWorkAsyncCoreUnitOfWorkAsync(
        IDbContextProvider<TContext> dbContextProvider,
        ILogger<EntityFrameWorkAsyncCoreUnitOfWorkAsync<TContext>> logger,
        IServiceProvider serviceProvider)
    {
        _dbContextProvider = dbContextProvider;
        this.logger = logger;
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    ///     DisposeAsync
    /// </summary>
    /// <returns>ValueTask</returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     Gets the custom repository for the <typeparamref name="TRepository" />.
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    /// <returns>TRepository</returns>
    public async Task<TRepository> GetCustomRepositoryAsync<TRepository>() where TRepository : class, IRepository
    {
        if (_repositories.ContainsKey(typeof(TRepository)))
        {
            return (TRepository)_repositories[typeof(TRepository)];
        }

        var repository = _serviceProvider.GetRequiredService<TRepository>();
        await repository.InitDbContextAsync();
        _repositories.Add(typeof(TRepository), repository);
        return repository;
    }

    /// <summary>
    ///     Gets the specified repository for the <typeparamref name="TEntity" />.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns>IBasicRepository</returns>
    public async Task<IBasicRepository<TEntity>> GetBasicRepositoryAsync<TEntity>() where TEntity : class, IEntity
    {
        if (_repositories.ContainsKey(typeof(IBasicRepository<TEntity>)))
        {
            return (IBasicRepository<TEntity>)_repositories[typeof(IBasicRepository<TEntity>)];
        }

        var repository = _serviceProvider.GetRequiredService<IBasicRepository<TEntity>>();
        await repository.InitDbContextAsync();
        _repositories.Add(typeof(IBasicRepository<TEntity>), repository);
        return repository;
    }

    /// <summary>
    ///     Asynchronously commit all changes made in this unit of work to the database.
    /// </summary>
    /// <param name="action">action</param>
    /// <returns>TRepository</returns>
    public async Task<bool> CommitAsync(Func<Task> action)
    {
        if (_contextTransaction is null)
        {
            return true;
        }

        try
        {
            await action();
            await _contextTransaction.CommitAsync();
        }
        catch (Exception e)
        {
            await _contextTransaction.RollbackAsync();
            logger.LogError(e, "Error saving changes");
            return false;
        }

        return true;
    }

    /// <summary>
    ///     Asynchronously begin transaction to the database.
    /// </summary>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    /// <param name="isolationLevel">isolationLevel</param>
    /// <returns>Task</returns>
    public async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.RepeatableRead)
    {
        _context ??= await _dbContextProvider.GetOrCreateDbContextAsync();
        _contextTransaction = await _context.Database.BeginTransactionAsync(isolationLevel);
    }

    /// <summary>
    ///     Dispose
    /// </summary>
    /// <param name="disposing">disposing</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    protected virtual async Task DisposeAsync(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _repositories.Clear();
        }

        if (_contextTransaction != null)
        {
            await _contextTransaction.DisposeAsync();
        }

        if (_context != null)
        {
            await _context.DisposeAsync();
        }

        _disposed = true;
    }
}