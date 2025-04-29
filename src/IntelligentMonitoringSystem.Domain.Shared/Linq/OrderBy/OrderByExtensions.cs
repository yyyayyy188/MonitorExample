namespace IntelligentMonitoringSystem.Domain.Shared.Linq.OrderBy;

/// <summary>
///     链式排序
/// </summary>
public static class OrderByExtensions
{
    /// <summary>
    ///     排序
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="orderBy">orderBy</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>IOrderedQueryable<T /></returns>
    public static IOrderedQueryable<T> DynamicOrderBy<T>(this IQueryable<T> source, IOrderBy<T, dynamic> orderBy)
    {
        return source.OrderBy(orderBy.Expression);
    }

    /// <summary>
    ///     排序降序
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="orderBy">orderBy</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>IOrderedQueryable<T /></returns>
    public static IOrderedQueryable<T> DynamicOrderByDescending<T>(this IQueryable<T> source, IOrderBy<T, dynamic> orderBy)
    {
        return source.OrderByDescending(orderBy.Expression);
    }

    /// <summary>
    ///     二次排序
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="orderBy">orderBy</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>IOrderedQueryable<T /></returns>
    public static IOrderedQueryable<T> DynamicThenBy<T>(this IOrderedQueryable<T> source, IOrderBy<T, dynamic> orderBy)
    {
        return source.ThenBy(orderBy.Expression);
    }

    /// <summary>
    ///     二次排序降序
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="orderBy">orderBy</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>IOrderedQueryable<T /></returns>
    public static IOrderedQueryable<T> DynamicThenByDescending<T>(this IOrderedQueryable<T> source, IOrderBy<T, dynamic> orderBy)
    {
        return source.ThenByDescending(orderBy.Expression);
    }
}