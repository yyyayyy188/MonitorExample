using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;
using IntelligentMonitoringSystem.Infrastructure.Redis.PersonnelAccessRecords.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentMonitoringSystem.Infrastructure.Redis;

/// <summary>
///     Redis模块
/// </summary>
public static class RedisModule
{
    /// <summary>
    ///     添加基础服务.
    /// </summary>
    /// <param name="services">services.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddRedisInfrastructure(
        this IServiceCollection services)
    {
        services.AddTransient<IPersonnelAccessRecordDelayRepository, PersonnelAccessRecordDelayRepository>();
        return services;
    }
}