using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.Linq.OrderBy;
using IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore.Repositories;

/// <summary>
///     EntityFrameworkCore Basic Repository
/// </summary>
/// <typeparam name="TDbContext">TDbContext</typeparam>
/// <typeparam name="TEntity">TEntity</typeparam>
public class EntityFrameworkCoreBasicRepository<TDbContext, TEntity>
    : IBasicRepository<TEntity>
    where TDbContext : BaseDbContext
    where TEntity : class, IEntity
{
    private readonly IDbContextProvider<TDbContext> _dbContextProvider;

    /// <summary>
    ///     数据库上下文
    /// </summary>
    private TDbContext? dbContext;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EntityFrameworkCoreBasicRepository{TDbContext, TEntity}" /> class.
    ///     EntityFrameworkCore Basic Repository
    /// </summary>
    /// <param name="dbContextProvider">dbContextProvider</param>
    public EntityFrameworkCoreBasicRepository(IDbContextProvider<TDbContext> dbContextProvider)
    {
        _dbContextProvider = dbContextProvider;
    }

    /// <summary>
    ///     初始化数据库上下文
    /// </summary>
    /// <returns>Task</returns>
    public async Task InitDbContextAsync()
    {
        dbContext = await _dbContextProvider.GetOrCreateDbContextAsync();
    }

    /// <summary>
    ///     Gets an entity with given primary key or null if not found.
    /// </summary>
    /// <param name="id">id</param>
    /// <typeparam name="TKey">TKey</typeparam>
    /// <returns>TEntity</returns>
    public async Task<TEntity?> GetAsync<TKey>(TKey id)
        where TKey : notnull
    {
        return await ExecuteWithDbContextAsync(async baseDbContext => await baseDbContext.Set<TEntity>().FindAsync(id));
    }

    /// <summary>
    ///     是否存在记录
    /// </summary>
    /// <param name="predicate">predicate</param>
    /// <returns>bool</returns>
    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        if (predicate == null)
        {
            return true;
        }

        return await ExecuteWithDbContextAsync(async baseDbContext => await baseDbContext.Set<TEntity>().AnyAsync(predicate));
    }

    /// <summary>
    ///     Gets all entities.
    /// </summary>
    /// <returns>IEnumerable</returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await ExecuteWithDbContextAsync(async baseDbContext => await baseDbContext.Set<TEntity>().ToListAsync());
    }

    /// <summary>
    ///     Insert entity
    /// </summary>
    /// <param name="entity">entity</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>TEntity</returns>
    public async Task<TEntity> InsertAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default)
    {
        return await ExecuteWithDbContextAsync(async baseDbContext =>
        {
            await baseDbContext.AddAsync(entity, cancellationToken);
            await baseDbContext.SaveChangesAsync(cancellationToken);
            return entity;
        });
    }

    /// <summary>
    ///     Bulk inserts a new entity.
    /// </summary>
    /// <param name="entities">Inserted entities</param>
    /// <param name="cancellationToken">
    ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to
    ///     complete.
    /// </param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task<int> BulkInsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        return await ExecuteWithDbContextAsync(async baseDbContext =>
        {
            await baseDbContext.AddRangeAsync(entities, cancellationToken);
            return await baseDbContext.SaveChangesAsync(cancellationToken);
        });
    }

    /// <summary>
    ///     Update entity
    /// </summary>
    /// <param name="entity">entity</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>bool</returns>
    public async Task<bool> UpdateAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default)
    {
        return await ExecuteWithDbContextAsync(async baseDbContext =>
        {
            baseDbContext.Update(entity);
            return await baseDbContext.SaveChangesAsync(cancellationToken) > 0;
        });
    }

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
    public async Task<bool> UpdateAsync([NotNull] TEntity entity, Action<TEntity> action,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteWithDbContextAsync(async baseDbContext =>
        {
            baseDbContext.Attach(entity);
            action(entity);
            return await baseDbContext.SaveChangesAsync(cancellationToken) > 0;
        });
    }

    /// <summary>
    ///     Delete entity
    /// </summary>
    /// <param name="entity">entity</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>bool</returns>
    public async Task<bool> DeleteAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default)
    {
        return await ExecuteWithDbContextAsync(async baseDbContext =>
        {
            baseDbContext.Remove(entity);
            return await baseDbContext.SaveChangesAsync(cancellationToken) > 0;
        });
    }

    /// <summary>
    ///     Execute Sql
    /// </summary>
    /// <param name="sql">sql</param>
    /// <param name="parameters">parameters</param>
    /// <returns>int</returns>
    public async Task<int> ExecuteSqlAsync(string sql, params object[] parameters)
    {
        return await ExecuteWithDbContextAsync(async baseDbContext =>
        {
            var raw = await baseDbContext.Database.ExecuteSqlRawAsync(sql, parameters);
            return raw;
        });
    }

    /// <summary>
    ///     Execute Update
    /// </summary>
    /// <param name="predicate">predicate</param>
    /// <param name="setPropertyCalls">setPropertyCalls</param>
    /// <returns>int</returns>
    public async Task<int> ExecuteUpdateAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls)
    {
        return await ExecuteWithDbContextAsync(async baseDbContext =>
        {
            await baseDbContext.Set<TEntity>().Where(predicate).ExecuteUpdateAsync(setPropertyCalls);
            return await baseDbContext.SaveChangesAsync();
        });
    }

    /// <summary>
    ///     Execute Delete
    /// </summary>
    /// <param name="predicate">predicate</param>
    /// <returns>int</returns>
    public async Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await ExecuteWithDbContextAsync(async baseDbContext =>
        {
            await baseDbContext.Set<TEntity>().Where(predicate).ExecuteDeleteAsync();
            return await baseDbContext.SaveChangesAsync();
        });
    }

    /// <summary>
    ///     FirstOrDefault
    /// </summary>
    /// <param name="predicate">predicate</param>
    /// <param name="orderBy">orderBy</param>
    /// <param name="selector">selector</param>
    /// <returns>TEntity?</returns>
    public async Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        IOrderBy<TEntity, dynamic>? orderBy = null,
        Expression<Func<TEntity, TEntity>>? selector = null)
    {
        return await ExecuteWithDbContextAsync(async baseDbContext =>
        {
            var queryable = baseDbContext.Set<TEntity>().Where(predicate);
            if (orderBy != null)
            {
                queryable = orderBy.IsDescending
                    ? queryable.DynamicOrderByDescending(orderBy)
                    : queryable.DynamicOrderBy(orderBy);
            }

            if (selector != null)
            {
                queryable = queryable.Select(selector);
            }

            return await queryable.FirstOrDefaultAsync();
        });
    }

    /// <summary>
    ///     Query
    /// </summary>
    /// <param name="predicate">predicate</param>
    /// <param name="selector">selector</param>
    /// <returns>List<TEntity /></returns>
    public async Task<IEnumerable<TEntity>> QueryAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Expression<Func<TEntity, TEntity>>? selector = null)
    {
        return await ExecuteWithDbContextAsync(async baseDbContext =>
        {
            var dbSet = baseDbContext.Set<TEntity>();
            var queryable = dbSet.AsQueryable().AsNoTracking();
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }

            if (selector != null)
            {
                queryable = queryable.Select(selector);
            }

            return await queryable.ToListAsync();
        });
    }

    /// <summary>
    ///     分页查询
    /// </summary>
    /// <param name="pageSize">页大小</param>
    /// <param name="skipCount">跳过数量</param>
    /// <param name="predicate">筛选条件</param>
    /// <param name="orderBy">排序方式</param>
    /// <param name="selector">选择器</param>
    /// <returns>IEnumerable<TEntity /></returns>
    public async Task<List<TEntity>> PageAsync(int pageSize, int skipCount, Expression<Func<TEntity, bool>>? predicate = null,
        IOrderBy<TEntity, dynamic>? orderBy = null, Expression<Func<TEntity, TEntity>>? selector = null)
    {
        return await ExecuteWithDbContextAsync(async baseDbContext =>
        {
            var queryable = baseDbContext.Set<TEntity>().AsQueryable().AsNoTracking();
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }

            if (orderBy != null)
            {
                queryable = orderBy.IsDescending
                    ? queryable.DynamicOrderByDescending(orderBy)
                    : queryable.DynamicOrderBy(orderBy);
            }

            if (selector != null)
            {
                queryable = queryable.Select(selector);
            }

            return await queryable.Skip(skipCount).Take(pageSize).ToListAsync();
        });
    }

    /// <summary>
    ///     数量统计
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns>Task<int /></returns>
    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return await ExecuteWithDbContextAsync(async baseDbContext =>
        {
            var queryable = baseDbContext.Set<TEntity>().AsQueryable().AsNoTracking();
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }

            return await queryable.CountAsync();
        });
    }

    /// <summary>
    ///     抽象通用的数据库上下文处理逻辑
    /// </summary>
    /// <typeparam name="TResult">返回结果类型</typeparam>
    /// <param name="func">操作逻辑</param>
    /// <returns>TResult</returns>
    protected async Task<TResult> ExecuteWithDbContextAsync<TResult>(Func<TDbContext, Task<TResult>> func)
    {
        if (dbContext != null)
        {
            return await func(dbContext);
        }

        await using var baseDbContext = await _dbContextProvider.CreateDbContextAsync();
        return await func(baseDbContext);
    }

    /// <summary>
    ///     抽象通用的数据库上下文处理逻辑
    /// </summary>
    /// <param name="func">操作逻辑</param>
    /// <returns>TResult</returns>
    protected async Task ExecuteWithDbContextAsync(Func<TDbContext, Task> func)
    {
        if (dbContext != null)
        {
            await func(dbContext);
            return;
        }

        await using var baseDbContext = await _dbContextProvider.CreateDbContextAsync();
        await func(baseDbContext);
    }
}