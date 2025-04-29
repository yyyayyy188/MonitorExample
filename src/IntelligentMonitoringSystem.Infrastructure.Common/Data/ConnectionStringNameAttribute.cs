using System.Reflection;

namespace IntelligentMonitoringSystem.Infrastructure.Common.Data;

/// <summary>
///     用于标记数据库连接字符串名称
/// </summary>
public class ConnectionStringNameAttribute(string name) : Attribute
{
    /// <summary>
    ///     数据库连接字符串名称
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    ///     获取数据库连接字符串名称
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>string</returns>
    public static string GetConnStringName<T>()
    {
        return GetConnStringName(typeof(T));
    }

    /// <summary>
    ///     获取数据库连接字符串名称
    /// </summary>
    /// <param name="type">上下文类型</param>
    /// <returns>string</returns>
    public static string GetConnStringName(Type type)
    {
        var nameAttribute = type.GetTypeInfo().GetCustomAttribute<ConnectionStringNameAttribute>();

        if (nameAttribute == null)
        {
            return type.FullName!;
        }

        return nameAttribute.Name;
    }
}