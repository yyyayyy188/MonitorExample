using FluentValidation;
using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using MediatR;

namespace IntelligentMonitoringSystem.Application.Common.Behaviours;

/// <summary>
///     ValidationBehavior
/// </summary>
/// <param name="validators">validators</param>
/// <typeparam name="TRequest">TRequest</typeparam>
/// <typeparam name="TResponse">TResponse</typeparam>
public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, ICommand
{
    /// <summary>
    ///     Handle
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="next">next</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>TResponse</returns>
    /// <exception>
    ///     <cref>FormatValidationException</cref>
    /// </exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        if (failures.Count != 0)
        {
            throw new FormatValidationException(failures);
        }

        return await next();
    }
}