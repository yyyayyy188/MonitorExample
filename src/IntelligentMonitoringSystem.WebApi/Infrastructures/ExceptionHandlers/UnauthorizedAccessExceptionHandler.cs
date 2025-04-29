using IntelligentMonitoringSystem.WebApi.Infrastructures.Problems;

namespace IntelligentMonitoringSystem.WebApi.Infrastructures.ExceptionHandlers;

/// <summary>
///     CustomExceptionHandler
/// </summary>
public partial class CustomExceptionHandler
{
    /// <summary>
    ///     Status401Unauthorized
    /// </summary>
    /// <param name="httpContext">httpContext</param>
    /// <param name="exception">exception</param>
    private async static Task HandleUnauthorizedAccessException(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        httpContext.Response.ContentType = Json;
        await httpContext.Response.WriteAsJsonAsync(new CustomProblemDetails
        {
            Code = $"{StatusCodes.Status401Unauthorized}",
            Title = "Unauthorized",
            Detail = exception.Message,
            Status = StatusCodes.Status401Unauthorized,
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
            Instance = httpContext.Request.Path
        });
    }
}