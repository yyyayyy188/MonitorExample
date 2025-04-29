using MediatR;

namespace IntelligentMonitoringSystem.Application.Common.Contracts;

/// <summary>
///     命令
/// </summary>
/// <typeparam name="TResult"></typeparam>
public interface ICommand<out TResult> : IRequest<TResult>, ICommand
{
}

/// <summary>
///     命令
/// </summary>
public interface ICommand : IRequest
{
    /// <summary>
    ///     命令Id
    /// </summary>
    Guid Id { get; }
}