using Microsoft.Extensions.Logging;

namespace IntelligentMonitoringSystem.Domain.Shared.Exceptions;

public class UnAuthorizationException : CustomException
{
    /// <summary>
    ///     Creates a new <see cref="UnAuthorizationException" /> object.
    /// </summary>
    public UnAuthorizationException()
    {
        LogLevel = LogLevel.Warning;
    }

    /// <summary>
    ///     Creates a new <see cref="UnAuthorizationException" /> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    public UnAuthorizationException(string message)
        : base(message)
    {
        LogLevel = LogLevel.Warning;
        Code = "401";
    }

    /// <summary>
    ///     Severity of the exception.
    ///     Default: Warn.
    /// </summary>
    public LogLevel LogLevel { get; set; }

    /// <summary>
    ///     Error code.
    /// </summary>
    public string? Code { get; }
}