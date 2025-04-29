using Microsoft.Extensions.DependencyInjection;

namespace IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore.Repositories;

/// <summary>
///     EntityFrameWorkCoreRepositoryRegistrar
/// </summary>
public class EntityFrameWorkCoreRepositoryRegistrar
{
    /// <summary>
    ///     RegisterDefaultRepositories
    /// </summary>
    /// <param name="serviceCollection">serviceCollection</param>
    /// <typeparam name="TDbContext"></typeparam>
    public void RegisterDefaultRepositories<TDbContext>(IServiceCollection serviceCollection)
    {
        var dbContextType = typeof(TDbContext);
        foreach (var entityType in GetEntityTypes(dbContextType))
        {
            serviceCollection.AddDefaultRepository(
                entityType,
                typeof(EntityFrameworkCoreBasicRepository<,>).MakeGenericType(dbContextType, entityType));
        }
    }

    /// <summary>
    ///     加载所有实体
    /// </summary>
    /// <param name="dbContextType">dbContextType</param>
    /// <returns>IEnumerable</returns>
    private static IEnumerable<Type> GetEntityTypes(Type dbContextType)
    {
        return EntityFrameWorkCoreDbContextHelper.GetEntityTypes(dbContextType);
    }
}