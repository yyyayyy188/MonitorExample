using IntelligentMonitoringSystem.Infrastructure.Common.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore;

/// <summary>
///     EntityFrameworkCore
/// </summary>
public static class EntityFrameworkCoreInfrastructureModule
{
    /// <summary>
    ///     Add EntityFrameworkCore
    /// </summary>
    /// <param name="services">services</param>
    /// <param name="configuration">configuration</param>
    /// <param name="poolSize">连接池数量</param>
    /// <typeparam name="TDbContext">TDbContext</typeparam>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddEntityFrameworkCoreInfrastructure<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        int poolSize = 1024)
        where TDbContext : BaseDbContext
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringNameAttribute.GetConnStringName<TDbContext>()) ??
                               string.Empty;
        services.AddDbContextAndDefaultRepository<TDbContext>(
            o => o.UseMySql(connectionString, new MySqlServerVersion(ServerVersion.AutoDetect(connectionString))), poolSize);
        return services;
    }
}