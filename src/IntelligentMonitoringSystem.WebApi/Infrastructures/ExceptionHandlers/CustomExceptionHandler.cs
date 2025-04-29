using System.Net;
using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using IntelligentMonitoringSystem.WebApi.Infrastructures.Problems;
using Microsoft.AspNetCore.Diagnostics;

namespace IntelligentMonitoringSystem.WebApi.Infrastructures.ExceptionHandlers;

/// <summary>
///     CustomExceptionHandler
/// </summary>
/// <param name="hostEnvironment">hostEnvironment</param>
public partial class CustomExceptionHandler(IHostEnvironment hostEnvironment) : IExceptionHandler
{
    private const string Json = "application/json";
    private const string _errorId = "ErrorId";

    private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers = new()
    {
        { typeof(FormatValidationException), HandleValidationException },
        { typeof(UnAuthorizationException), HandleUnauthorizedAccessException },
        { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
        { typeof(UserFriendlyException), HandleUserFriendlyException },
        { typeof(BusinessException), HandleCustomException }
    };

    /// <summary>
    ///     Register known exception types and handlers.
    /// </summary>
    /// <param name="httpContext">httpContext</param>
    /// <param name="exception">exception</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>ValueTask</returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var exceptionType = exception.GetType();

        if (_exceptionHandlers.TryGetValue(exceptionType, out _))
        {
            await _exceptionHandlers[exceptionType].Invoke(httpContext, exception);
            return true;
        }

        await HandleExceptionAsync(httpContext, exception, _errorId);
        return true;
    }

    /// <summary>
    ///     HandleCustomException
    /// </summary>
    /// <param name="httpContext">httpContext</param>
    /// <param name="ex">ex</param>
    /// <returns>Task</returns>
    private async static Task HandleCustomException(HttpContext httpContext, Exception ex)
    {
        var exception = ex as BusinessException;

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = Json;

        await httpContext.Response.WriteAsJsonAsync(new CustomProblemDetails
        {
            Code = exception?.Code ?? $"{StatusCodes.Status500InternalServerError}",
            Status = StatusCodes.Status500InternalServerError,
            Message = exception?.Message,
            Title = "InternalServerError",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
        });
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception, string errorId)
    {
        var message =
            $"There are some exception in our system, We apologize for your inconvenience，please contact with our system administrator.[ErrorId:{errorId}]";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = Json;

        await httpContext.Response.WriteAsJsonAsync(new CustomProblemDetails
        {
            Code = $"{StatusCodes.Status500InternalServerError}",
            Status = StatusCodes.Status500InternalServerError,
            Message = hostEnvironment.IsProduction() ? message : exception.Message,
            Title = "InternalServerError",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
        });
    }
}