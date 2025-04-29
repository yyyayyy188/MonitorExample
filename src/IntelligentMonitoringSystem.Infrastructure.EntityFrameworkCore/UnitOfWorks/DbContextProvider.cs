namespace IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore.UnitOfWorks;

/// <summary>
///     数据库上下文提供者
/// </summary>
/// <param name="factory">factory</param>
/// <typeparam name="TContext">TContext</typeparam>
public class DbContextProvider<TContext>(BaseDbContextFactory<TContext> factory)
    : IDbContextProvider<TContext> where TContext : BaseDbContext
{
    private TContext? _context;

    /// <summary>
    ///     获取或创建数据库上下文
    /// </summary>
    /// <returns>TContext</returns>
    public async Task<TContext> GetOrCreateDbContextAsync()
    {
        if (_context != null)
        {
            return _context;
        }

        _context = await factory.CreateDbContextAsync();
        return _context;
    }

    /// <summary>
    ///     创建数据库上下文
    /// </summary>
    /// <returns>TContext</returns>
    public async Task<TContext> CreateDbContextAsync()
    {
        return await factory.CreateDbContextAsync();
    }
}