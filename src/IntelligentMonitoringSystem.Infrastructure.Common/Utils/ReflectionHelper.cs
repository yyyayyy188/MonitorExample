using System.Reflection;

namespace IntelligentMonitoringSystem.Infrastructure.Common.Utils;

/// <summary>
///     Reflection helper
/// </summary>
public static class ReflectionHelper
{
    /// <summary>
    ///     Checks whether <paramref name="givenType" /> implements/inherits <paramref name="genericType" />.
    /// </summary>
    /// <param name="givenType">Type to check.</param>
    /// <param name="genericType">Generic type.</param>
    /// <returns>bool.</returns>
    public static bool IsAssignableToGenericType(Type givenType, Type genericType)
    {
        var givenTypeInfo = givenType.GetTypeInfo();

        if (givenTypeInfo.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
        {
            return true;
        }

        if (givenTypeInfo.GetInterfaces().Any(interfaceType => interfaceType.GetTypeInfo().IsGenericType &&
                                                               interfaceType.GetGenericTypeDefinition() == genericType))
        {
            return true;
        }

        return givenTypeInfo.BaseType != null && IsAssignableToGenericType(givenTypeInfo.BaseType, genericType);
    }
}