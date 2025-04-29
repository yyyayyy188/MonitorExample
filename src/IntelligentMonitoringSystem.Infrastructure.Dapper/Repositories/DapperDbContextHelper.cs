using System.Reflection;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using IntelligentMonitoringSystem.Infrastructure.Common.Utils;
using IntelligentMonitoringSystem.Infrastructure.Dapper.DbContexts;

namespace IntelligentMonitoringSystem.Infrastructure.Dapper.Repositories;

/// <summary>
///     Helper class to get entity types from a DbContext
/// </summary>
public static class DapperDbContextHelper
{
    /// <summary>
    ///     Get all entity types from a DbContext
    /// </summary>
    /// <param name="dbContextType">dbContextType</param>
    /// <returns>IEnumerable Type</returns>
    public static IEnumerable<Type> GetEntityTypes(Type dbContextType)
    {
        return
            from property in dbContextType.GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            where
                ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(DapperDbSet<>)) &&
                typeof(IEntity).IsAssignableFrom(property.PropertyType.GenericTypeArguments[0])
            select property.PropertyType.GenericTypeArguments[0];
    }
}