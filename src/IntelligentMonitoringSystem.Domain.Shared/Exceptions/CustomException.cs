namespace IntelligentMonitoringSystem.Domain.Shared.Exceptions;

/// <summary>
///     CustomException
/// </summary>
public class CustomException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CustomException" /> class.
    ///     CustomException
    /// </summary>
    public CustomException()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="CustomException" /> class.
    /// </summary>
    /// <param name="message">message</param>
    public CustomException(string? message)
        : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="CustomException" /> class.
    ///     CustomException
    /// </summary>
    /// <param name="message">message</param>
    /// <param name="innerException">innerException</param>
    public CustomException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}