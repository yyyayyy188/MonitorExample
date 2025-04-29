using System.Text.Json;
using FluentValidation;
using IntelligentMonitoringSystem.Domain.Shared.Exceptions;

namespace IntelligentMonitoringSystem.WebApi.Validators;

/// <summary>
///     ValidationMiddleware
/// </summary>
/// <param name="next">next.</param>
/// <param name="validator">validator.</param>
/// <typeparam name="T">T.</typeparam>
public class ValidationMiddleware<T>(RequestDelegate next, IValidator<T> validator)
    where T : class
{
    /// <summary>
    ///     Invoke.
    /// </summary>
    /// <param name="context">context</param>
    /// <exception cref="FormatValidationException">FormatValidationException</exception>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task Invoke(HttpContext context)
    {
        context.Request.EnableBuffering();
        var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
        context.Request.Body.Position = 0;

        var model = JsonSerializer.Deserialize<T>(body);
        if (model != null)
        {
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                throw new FormatValidationException(validationResult.Errors);
            }
        }

        await next(context);
    }
}