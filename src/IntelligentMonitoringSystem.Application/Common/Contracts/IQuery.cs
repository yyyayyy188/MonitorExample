using MediatR;

namespace IntelligentMonitoringSystem.Application.Common.Contracts;

/// <summary>
///     查询处理器
/// </summary>
/// <typeparam name="TResult"></typeparam>
public interface IQuery<out TResult> : IRequest<TResult>
{
}