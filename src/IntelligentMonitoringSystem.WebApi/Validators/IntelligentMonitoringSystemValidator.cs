using FluentValidation;

namespace IntelligentMonitoringSystem.WebApi.Validators;

/// <summary>
///     IIntelligentMonitoringSystemValidator
/// </summary>
public interface IIntelligentMonitoringSystemValidator;

/// <summary>
///     IntelligentMonitoringSystemValidatorExtension
/// </summary>
public static class IntelligentMonitoringSystemValidatorExtension
{
    /// <summary>
    ///     AddIntelligentMonitoringSystemFluentValidation
    /// </summary>
    /// <param name="services">services</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddIntelligentMonitoringSystemFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<IIntelligentMonitoringSystemValidator>(ServiceLifetime.Transient);
        return services;
    }
}