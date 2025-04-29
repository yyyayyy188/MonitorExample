using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.Common.Contracts;

/// <summary>
///     Base class for all commands.
/// </summary>
/// <param name="id">id</param>
public abstract class CommandBase(Guid id) : ICommand
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CommandBase" /> class.
    ///     CommandBase
    /// </summary>
    protected CommandBase()
        : this(Guid.NewGuid())
    {
    }

    /// <summary>
    ///     Id
    /// </summary>
    [JsonIgnore]
    public Guid Id { get; } = id;
}

/// <summary>
///     CommandBase
/// </summary>
/// <param name="id">id</param>
/// <typeparam name="TResult"></typeparam>
public abstract class CommandBase<TResult>(Guid id) : ICommand<TResult>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CommandBase{TResult}" /> class.
    ///     CommandBase
    /// </summary>
    protected CommandBase()
        : this(Guid.NewGuid())
    {
    }

    /// <summary>
    ///     Id
    /// </summary>
    [JsonIgnore]
    public Guid Id { get; } = id;
}