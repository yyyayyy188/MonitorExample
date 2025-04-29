using Microsoft.EntityFrameworkCore;

namespace IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore;

/// <summary>
///     创建DbContext
/// </summary>
/// <param name="contextInFactory">contextInFactory</param>
public class BaseDbContextFactory<TDbContext>(IDbContextFactory<TDbContext> contextInFactory)
    : IDbContextFactory<TDbContext> where TDbContext : DbContext
{
    /// <inheritdoc />
    public TDbContext CreateDbContext()
    {
        return contextInFactory.CreateDbContext();
    }

    /// <inheritdoc />
    public async Task<TDbContext> CreateDbContextAsync(CancellationToken cancellationToken = default)
    {
        return await contextInFactory.CreateDbContextAsync(cancellationToken);
    }
}