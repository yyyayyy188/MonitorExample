using FluentValidation.Results;

namespace IntelligentMonitoringSystem.Domain.Shared.Exceptions;

/// <summary>
///     Validation exception.
/// </summary>
public class FormatValidationException() : BusinessException("One or more validation failures have occurred.", null)
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="FormatValidationException" /> class.
    /// </summary>
    /// <param name="failures">failures</param>
    public FormatValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages) => new { Key = propertyName, Values = errorMessages.Distinct().ToArray() })
            .ToDictionary(x => x.Key, x => x.Values);
    }

    /// <summary>
    ///     Gets the errors.
    /// </summary>
    public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();
}