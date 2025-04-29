namespace IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore.UnitOfWorks;

/// <summary>
///     数据库上下文提供者
/// </summary>
/// <typeparam name="TContext"></typeparam>
public interface IDbContextProvider<TContext>
    where TContext : BaseDbContext
{
    /// <summary>
    ///     获取或创建数据库上下文
    /// </summary>
    /// <returns>TContext</returns>
    Task<TContext> GetOrCreateDbContextAsync();

    /// <summary>
    ///     创建数据库上下文
    /// </summary>
    /// <returns>TContext</returns>
    Task<TContext> CreateDbContextAsync();
}