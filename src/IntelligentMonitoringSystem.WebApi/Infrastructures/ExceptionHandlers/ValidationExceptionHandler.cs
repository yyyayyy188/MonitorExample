using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using IntelligentMonitoringSystem.WebApi.Infrastructures.Problems;

namespace IntelligentMonitoringSystem.WebApi.Infrastructures.ExceptionHandlers;

/// <summary>
///     CustomExceptionHandler
/// </summary>
public partial class CustomExceptionHandler
{
    /// <summary>
    ///     Status403Forbidden
    /// </summary>
    /// <param name="httpContext">httpContext</param>
    /// <param name="ex">ex</param>
    private async static Task HandleValidationException(HttpContext httpContext, Exception ex)
    {
        var exception = ex as FormatValidationException;
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        httpContext.Response.ContentType = Json;

        await httpContext.Response.WriteAsJsonAsync(new CustomProblemDetails
        {
            Type = "ValidationFailure",
            Title = "Validation error",
            Detail = "One or more validation errors has occurred",
            Code = exception?.Code ?? $"{StatusCodes.Status400BadRequest}",
            Status = StatusCodes.Status400BadRequest,
            Message = exception?.Errors.FirstOrDefault().Value?.FirstOrDefault() ?? string.Empty
        });
    }
}