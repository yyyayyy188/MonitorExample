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
    private async static Task HandleForbiddenAccessException(HttpContext httpContext, Exception ex)
    {
        var exception = ex as ForbiddenAccessException;

        httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
        httpContext.Response.ContentType = Json;

        await httpContext.Response.WriteAsJsonAsync(new CustomProblemDetails
        {
            Code = $"{StatusCodes.Status403Forbidden}",
            Status = StatusCodes.Status403Forbidden,
            Message = exception?.Message,
            Title = "Forbidden",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
        });
    }
}