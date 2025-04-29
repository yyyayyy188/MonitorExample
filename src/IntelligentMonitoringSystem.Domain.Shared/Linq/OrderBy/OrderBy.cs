using System.Linq.Expressions;

namespace IntelligentMonitoringSystem.Domain.Shared.Linq.OrderBy;

/// <summary>
///     排序
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TKey"></typeparam>
public class OrderBy<TSource, TKey> : IOrderBy<TSource, TKey>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OrderBy{TSource, TKey}" /> class.
    ///     构造函数
    /// </summary>
    /// <param name="expression">expression</param>
    /// <param name="isDescending">isDescending</param>
    public OrderBy(Expression<Func<TSource, TKey>> expression, bool isDescending = false)
    {
        Expression = expression;
        IsDescending = isDescending;
    }

    /// <summary>
    ///     是否降序
    /// </summary>
    public bool IsDescending { get; }

    /// <summary>
    ///     排序表达式
    /// </summary>
    public Expression<Func<TSource, TKey>> Expression { get; }
}