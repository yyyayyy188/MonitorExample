using MediatR;

namespace IntelligentMonitoringSystem.Application.Common.Contracts;

/// <summary>
///     同步命令
/// </summary>
/// <typeparam name="TCommand">TCommand</typeparam>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
}

/// <summary>
///     异步命令处理器
/// </summary>
/// <typeparam name="TCommand">TCommand</typeparam>
/// <typeparam name="TResult">TResult</typeparam>
public interface ICommandHandler<in TCommand, TResult> :
    IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
}