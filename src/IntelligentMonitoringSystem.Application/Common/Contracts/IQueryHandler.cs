using MediatR;

namespace IntelligentMonitoringSystem.Application.Common.Contracts;

/// <summary>
///     查询处理器
/// </summary>
/// <typeparam name="TQuery">TQuery</typeparam>
/// <typeparam name="TResult">TResult</typeparam>
public interface IQueryHandler<in TQuery, TResult> :
    IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
}