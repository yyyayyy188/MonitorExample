using IntelligentMonitoringSystem.Application.Common.Behaviours;
using IntelligentMonitoringSystem.Application.MessageCenters.MessageDataResolvers;
using IntelligentMonitoringSystem.Application.MessageCenters.MessageDataResolvers.Impl;
using IntelligentMonitoringSystem.Domain;
using IntelligentMonitoringSystem.Domain.Shared.MessageCenters.Const;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentMonitoringSystem.Application;

/// <summary>
///     应用层
/// </summary>
public static class ApplicationModule
{
    /// <summary>
    ///     添加应用层
    /// </summary>
    /// <param name="services">services</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(ApplicationModule).Assembly, typeof(DomainModule).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
        services.AddIntelligentMonitoringSystemAutoMapper();
        services.AddKeyedTransient<IMessageDataResolver, AccessRecordMessageDataResolver>(MessageDataResolverConst
            .AccessRecordMessageDataResolverProvider);
        services.AddKeyedTransient<IMessageDataResolver, AbnormalWaringMessageDataResolverProvider>(MessageDataResolverConst
            .AbnormalWaringMessageDataResolverProvider);
        return services;
    }
}