using System.Linq.Expressions;

namespace IntelligentMonitoringSystem.Domain.Shared.Linq.OrderBy;

/// <summary>
///     排序
/// </summary>
public interface IOrderBy<TSource, TKey>
{
    /// <summary>
    ///     是否降序
    /// </summary>
    public bool IsDescending { get; }

    /// <summary>
    ///     表达式
    /// </summary>
    Expression<Func<TSource, TKey>> Expression { get; }
}