using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using IntelligentMonitoringSystem.WebApi.Infrastructures.Problems;

namespace IntelligentMonitoringSystem.WebApi.Infrastructures.ExceptionHandlers;

/// <summary>
///     CustomExceptionHandler
/// </summary>
public partial class CustomExceptionHandler
{
    /// <summary>
    ///     Status400BadRequest
    /// </summary>
    /// <param name="httpContext">httpContext</param>
    /// <param name="ex">ex</param>
    private async static Task HandleUserFriendlyException(HttpContext httpContext, Exception ex)
    {
        var exception = ex as UserFriendlyException;

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        httpContext.Response.ContentType = Json;

        await httpContext.Response.WriteAsJsonAsync(new CustomProblemDetails
        {
            Code = exception?.Code ?? $"{StatusCodes.Status400BadRequest}",
            Status = StatusCodes.Status400BadRequest,
            Message = exception?.Message,
            Title = "BadRequest",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1"
        });
    }
}