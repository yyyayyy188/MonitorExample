using Serilog.Context;

namespace IntelligentMonitoringSystem.WebApi.Infrastructures.Middlewares;

/// <summary>
///     RequestLogContextMiddleware
/// </summary>
/// <param name="next">next</param>
public class RequestLogContextMiddleware(RequestDelegate next)
{
    /// <summary>
    ///     Invoke
    /// </summary>
    /// <param name="context">context</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task Invoke(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
        {
            await next(context);
        }
    }
}