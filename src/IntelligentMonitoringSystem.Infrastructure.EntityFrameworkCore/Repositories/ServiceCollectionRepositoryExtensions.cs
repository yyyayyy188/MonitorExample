using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore.Repositories;

/// <summary>
///     ServiceCollection extension methods for adding repositories.
/// </summary>
public static class ServiceCollectionRepositoryExtensions
{
    /// <summary>
    ///     Add default repository
    /// </summary>
    /// <param name="services">services</param>
    /// <param name="entityType">entityType</param>
    /// <param name="repositoryImplementationType">repositoryImplementationType</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddDefaultRepository(this IServiceCollection services, Type entityType,
        Type repositoryImplementationType)
    {
        var basicRepository = typeof(IBasicRepository<>).MakeGenericType(entityType);
        if (basicRepository.IsAssignableFrom(repositoryImplementationType))
        {
            RegisterService(services, basicRepository, repositoryImplementationType);
        }

        return services;
    }

    private static void RegisterService(IServiceCollection services, Type serviceType, Type implementationType)
    {
        services.TryAddTransient(serviceType, implementationType);
    }
}