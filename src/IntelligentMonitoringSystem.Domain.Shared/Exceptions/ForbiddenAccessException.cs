using Microsoft.Extensions.Logging;

namespace IntelligentMonitoringSystem.Domain.Shared.Exceptions;

/// <summary>
///     Forbidden access exception.
/// </summary>
public class ForbiddenAccessException : CustomException
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ForbiddenAccessException" /> class.
    ///     Creates a new <see cref="ForbiddenAccessException" /> object.
    /// </summary>
    public ForbiddenAccessException()
    {
        LogLevel = LogLevel.Warning;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ForbiddenAccessException" /> class.
    ///     Creates a new <see cref="ForbiddenAccessException" /> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    public ForbiddenAccessException(string message)
        : base(message)
    {
        LogLevel = LogLevel.Warning;
        Code = "403";
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