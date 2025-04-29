using FluentValidation;
using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IntelligentMonitoringSystem.WebApi.Validators;

/// <summary>
///     ValidationFilter
/// </summary>
/// <param name="validator">validator.</param>
/// <typeparam name="T"></typeparam>
public class ValidationFilter<T>(IValidator<T> validator) : IActionFilter, IAsyncActionFilter
    where T : class
{
    /// <summary>
    ///     Sync Validate
    /// </summary>
    /// <param name="context">context.</param>
    /// <exception cref="FormatValidationException">FormatValidationException.</exception>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.Values.FirstOrDefault(v => v is T) is not T argument)
        {
            return;
        }

        var validationResult = validator.Validate(argument);
        if (!validationResult.IsValid)
        {
            throw new FormatValidationException(validationResult.Errors);
        }
    }

    /// <summary>
    ///     Action Executed
    /// </summary>
    /// <param name="context">context</param>
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    /// <summary>
    ///     Async Validate
    /// </summary>
    /// <param name="context">context</param>
    /// <param name="next">next</param>
    /// <exception cref="FormatValidationException">FormatValidationException</exception>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.Values.FirstOrDefault(v => v is T) is T argument)
        {
            var validationResult = await validator.ValidateAsync(argument);
            if (!validationResult.IsValid)
            {
                throw new FormatValidationException(validationResult.Errors);
            }
        }

        await next();
    }
}