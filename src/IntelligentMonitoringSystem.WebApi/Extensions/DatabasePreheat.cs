using IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IntelligentMonitoringSystem.WebApi.Extensions;

/// <summary>
///     数据库预热
/// </summary>
public static class DatabasePreheat
{
    /// <summary>
    ///     预热数据库
    /// </summary>
    /// <param name="applicationBuilder">applicationBuilder</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async static Task PreheatAsync(this IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        var intelligentMonitoringSystemDbContext = serviceScope.ServiceProvider
            .GetRequiredService<IDbContextFactory<IntelligentMonitoringSystemDbContext>>();
        await using var dbContext = await intelligentMonitoringSystemDbContext.CreateDbContextAsync();
        await dbContext.PersonnelAccessRecords.AnyAsync();
    }
}