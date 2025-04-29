using Microsoft.Extensions.DependencyInjection;

namespace IntelligentMonitoringSystem.Infrastructure.Dapper.Repositories;

/// <summary>
///     Register default registrar
/// </summary>
public class DapperRepositoryRegistrar
{
    /// <summary>
    ///     Load all DbTypes from dbContextType folder
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <param name="serviceCollection">serviceCollection</param>
    public void RegisterDefaultRepositories<TDbContext>(IServiceCollection serviceCollection)
    {
        var dbContextType = typeof(TDbContext);
        foreach (var entityType in GetEntityTypes(dbContextType))
        {
            serviceCollection.AddDefaultRepository(
                entityType,
                typeof(DapperReadOnlyBasicRepository<,>).MakeGenericType(dbContextType, entityType));
        }
    }

    /// <summary>
    ///     Load entity types
    /// </summary>
    /// <param name="dbContextType">dbContextType</param>
    /// <returns>IEnumerable</returns>
    private IEnumerable<Type> GetEntityTypes(Type dbContextType)
    {
        return DapperDbContextHelper.GetEntityTypes(dbContextType);
    }
}