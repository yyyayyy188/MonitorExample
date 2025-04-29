using System.Reflection;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using IntelligentMonitoringSystem.Infrastructure.Common.Utils;
using Microsoft.EntityFrameworkCore;

namespace IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore.Repositories;

/// <summary>
///     EntityFrameWorkCoreDbContextHelper
/// </summary>
public static class EntityFrameWorkCoreDbContextHelper
{
    /// <summary>
    ///     Get Entity T
    /// </summary>
    /// <param name="dbContextType">dbContextType</param>
    /// <returns>IEnumerable</returns>
    public static IEnumerable<Type> GetEntityTypes(Type dbContextType)
    {
        var types =
            from property in dbContextType.GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            where
                ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>)) &&
                typeof(IEntity).IsAssignableFrom(property.PropertyType.GenericTypeArguments[0])
            select property.PropertyType.GenericTypeArguments[0];
        return types;
    }
}