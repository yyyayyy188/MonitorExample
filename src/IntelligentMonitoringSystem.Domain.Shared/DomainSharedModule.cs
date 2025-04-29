using MassTransit;
using MassTransit.NewIdProviders;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentMonitoringSystem.Domain.Shared;

/// <summary>
///     域共享模块
/// </summary>
public static class DomainSharedModule
{
    /// <summary>
    ///     添加域共享模块
    /// </summary>
    /// <param name="services">services</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddDomainSharedModule(this IServiceCollection services)
    {
        // 设置全局唯一ID
        NewId.SetProcessIdProvider(new CurrentProcessIdProvider());
        return services;
    }
}