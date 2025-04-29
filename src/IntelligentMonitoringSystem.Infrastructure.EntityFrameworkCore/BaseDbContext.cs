using Microsoft.EntityFrameworkCore;

namespace IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore;

/// <summary>
///     基础DbContext
/// </summary>
/// <param name="options">options</param>
public class BaseDbContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>
    ///     是否启用事务
    /// </summary>
    public bool IsEnableTransaction { get; set; }
}