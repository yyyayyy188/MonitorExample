using System.Linq.Dynamic.Core;
using Ardalis.GuardClauses;

namespace IntelligentMonitoringSystem.Domain.Shared.Extensions;

/// <summary>
///     查询扩展
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    ///     分页
    /// </summary>
    /// <param name="query">query</param>
    /// <param name="skipCount">skipCount</param>
    /// <param name="maxResultCount">maxResultCount</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>IQueryable<T /></returns>
    public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int maxResultCount)
    {
        Guard.Against.Null(query);
        return query.Skip(skipCount).Take(maxResultCount);
    }

    /// <summary>
    ///     Order a <see cref="IQueryable" /> by given predicate if given condition is true.
    /// </summary>
    /// <param name="query">Queryable to apply filtering</param>
    /// <param name="condition">A boolean value</param>
    /// <param name="sorting">Order the query</param>
    /// <returns>Order or not order query based on <paramref name="condition" /></returns>
    public static IQueryable<T> OrderByIf<T>(this IQueryable<T> query, bool condition, string sorting)
    {
        Guard.Against.Null(query);

        return condition
            ? DynamicQueryableExtensions.OrderBy(query, sorting)
            : query;
    }
}