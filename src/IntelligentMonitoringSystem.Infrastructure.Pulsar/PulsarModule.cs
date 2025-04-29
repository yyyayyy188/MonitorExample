using DotPulsar;
using DotPulsar.Abstractions;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Const;
using IntelligentMonitoringSystem.Infrastructure.Pulsar.PersonnelAccessRecords.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentMonitoringSystem.Infrastructure.Pulsar;

/// <summary>
///     Pulsar 模块.
/// </summary>
public static class PulsarModule
{
    /// <summary>
    ///     添加基础服务.
    /// </summary>
    /// <param name="services">services.</param>
    /// <param name="configuration">configuration</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddPulsarInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(_ =>
        {
            var pulsarClient = PulsarClient
                .Builder()
                .ServiceUrl(new Uri(configuration["Pulsar:ServiceUrl"] ?? string.Empty))
                .RetryInterval(
                    TimeSpan.FromMilliseconds(
                        Convert.ToInt32(configuration["Pulsar:RetryInterval"])))
                .Build();
            return pulsarClient;
        });

        services.AddKeyedSingleton<IProducer<string>>(
            PersonnelAccessRecordConst.TopicName,
            (provider, _) =>
            {
                var pulsarClient = provider.GetRequiredService<IPulsarClient>();
                var options = new ProducerOptions<string>(PersonnelAccessRecordConst.TopicName, Schema.String);
                var producer = pulsarClient.CreateProducer(options);

                return producer;
            });

        services.AddTransient<IPersonnelAccessRecordDelayRepository, PersonnelAccessRecordDelayRepository>();
        return services;
    }
}