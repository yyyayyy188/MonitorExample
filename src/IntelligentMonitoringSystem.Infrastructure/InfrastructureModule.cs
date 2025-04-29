using IntelligentMonitoringSystem.Infrastructure.Dapper;
using IntelligentMonitoringSystem.Infrastructure.Dapper.DbContexts;
using IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore;
using IntelligentMonitoringSystem.Infrastructure.FFMpeg;
using IntelligentMonitoringSystem.Infrastructure.Pulsar;
using IntelligentMonitoringSystem.Infrastructure.Reference;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentMonitoringSystem.Infrastructure;

/// <summary>
///     InfrastructureModule
/// </summary>
public static class InfrastructureModule
{
    /// <summary>
    ///     Adds the infrastructure layer to the service collection.
    /// </summary>
    /// <param name="services">services</param>
    /// <param name="configuration">configuration</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddPulsarInfrastructure(configuration);
        services.AddDapperInfrastructure<DapperDbContext>(configuration);
        services.AddEntityFrameworkCoreInfrastructure<IntelligentMonitoringSystemDbContext>(configuration, 50);
        services.AddReferenceInfrastructure();
        services.AddFFMpegModule(configuration);
        return services;
    }
}