using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IntelligentMonitoringSystem.Infrastructure.Dapper.Repositories;

/// <summary>
///     Service collection extension for Dapper repositories.
/// </summary>
public static class ServiceCollectionRepositoryExtensions
{
    /// <summary>
    ///     Adds default repository implementations to the service collection.
    /// </summary>
    /// <param name="services">services</param>
    /// <param name="entityType">entityType</param>
    /// <param name="readOnlyRepositoryImplementationType">readOnlyRepositoryImplementationType</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddDefaultRepository(this IServiceCollection services, Type entityType,
        Type readOnlyRepositoryImplementationType)
    {
        var readOnlyBasicRepository = typeof(IReadOnlyBasicRepository<>).MakeGenericType(entityType);
        if (readOnlyBasicRepository.IsAssignableFrom(readOnlyBasicRepository))
        {
            RegisterService(services, readOnlyBasicRepository, readOnlyRepositoryImplementationType);
        }

        return services;
    }

    private static void RegisterService(IServiceCollection services, Type serviceType,
        Type implementationType)
    {
        services.TryAddTransient(serviceType, implementationType);
    }
}