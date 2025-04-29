using Microsoft.Extensions.Logging;

namespace IntelligentMonitoringSystem.Domain.Shared.Exceptions;

/// <summary>
///     The base class for all business exceptions.
/// </summary>
public class BusinessException : CustomException, IBusinessException
{
    private const string _code = "500";

    /// <summary>
    ///     Initializes a new instance of the <see cref="BusinessException" /> class.
    /// </summary>
    /// <param name="code">code</param>
    /// <param name="message">message</param>
    /// <param name="logLevel">logLevel</param>
    public BusinessException(
        string? code = null,
        string? message = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(message)
    {
        Code = code;
        LogLevel = logLevel;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="BusinessException" /> class.
    ///     The code to be used when logging this exception.
    /// </summary>
    /// <param name="message">message</param>
    /// <param name="logLevel">logLevel</param>
    public BusinessException(
        string? message = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(message)
    {
        Code = _code;
        LogLevel = logLevel;
    }

    /// <summary>
    ///     The code to be used when logging this exception.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    ///     The log level to be used when logging this exception.
    /// </summary>
    public LogLevel LogLevel { get; set; }
}