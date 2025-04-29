using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.Common.Contracts;

/// <summary>
///     Base class for all queries.
/// </summary>
/// <param name="id">id</param>
/// <typeparam name="TResult">TResult</typeparam>
public abstract class QueryBase<TResult>(Guid id) : IQuery<TResult>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryBase{TResult}" /> class.
    /// </summary>
    protected QueryBase() : this(Guid.NewGuid())
    {
    }

    /// <summary>
    ///     Id
    /// </summary>
    [JsonIgnore]
    public Guid Id { get; } = id;
}