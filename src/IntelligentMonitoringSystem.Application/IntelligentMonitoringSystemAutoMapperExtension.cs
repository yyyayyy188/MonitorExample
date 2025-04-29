using Microsoft.Extensions.DependencyInjection;

namespace IntelligentMonitoringSystem.Application;

/// <summary>
///     智能监控系统自动映射
/// </summary>
public static class IntelligentMonitoringSystemAutoMapperExtension
{
    /// <summary>
    ///     添加智能监控系统自动映射
    /// </summary>
    /// <param name="services">services</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddIntelligentMonitoringSystemAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ApplicationModule));
        return services;
    }
}