// <copyright file="BackgroundServiceModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Coravel;
using DotPulsar;
using IntelligentMonitoringSystem.BackgroundService.BackgroundServices;
using IntelligentMonitoringSystem.BackgroundService.BackgroundServices.Queue;
using IntelligentMonitoringSystem.BackgroundService.Invocable.EventSources;
using IntelligentMonitoringSystem.Domain.Shared.Configs;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Configs;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.NewtonsoftJson;

namespace IntelligentMonitoringSystem.BackgroundService;

/// <summary>
///     后台服务模块
/// </summary>
public static class BackgroundServiceModule
{
    /// <summary>
    ///     Add WebApi.
    /// </summary>
    /// <param name="services">services.</param>
    /// <param name="configurationBuilder">configurationBuilder.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddBackgroundService(
        this IServiceCollection services,
        IHostApplicationBuilder configurationBuilder)
    {
        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var conn = configurationBuilder.Configuration["Redis:ConnectionString"] ?? string.Empty;
            return ConnectionMultiplexer.Connect(conn);
        });

        services.AddSingleton(_ =>
        {
            var pulsarClient = PulsarClient
                .Builder()
                .ServiceUrl(new Uri(configurationBuilder.Configuration["Pulsar:ServiceUrl"] ?? string.Empty))
                .RetryInterval(
                    TimeSpan.FromMilliseconds(
                        Convert.ToInt32(configurationBuilder.Configuration["Pulsar:RetryInterval"])))
                .Build();
            return pulsarClient;
        });
        services.AddSingleton<IBlockQueue, BlockQueue>();
        services.AddHostedService<SynchronizationVideoPlaybackEventBackgroundService>();
        services.Configure<HostOptions>(hostOptions =>
        {
            hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
        });

        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        });
        services.AddEndpointsApiExplorer();
        services.AddHealthChecks();

        services.Configure<PersonnelAccessRecordConfig>(
            configurationBuilder.Configuration.GetSection(PersonnelAccessRecordConfig.PositionOptions));

        services.Configure<IntelligentMonitorSystemHttpClientConfig>(
            configurationBuilder.Configuration.GetSection(IntelligentMonitorSystemHttpClientConfig.PositionOptions));

        services.AddScheduler();
        services.AddTransient<EventSourceProcessInvocable>();

        services.Configure<FFMpegOptions>(
            x =>
            {
                x.BinaryFolder = configurationBuilder.Configuration["FFMpeg:BinaryFolder"] ?? string.Empty;
                x.BaseVideoPath =
                    $"{configurationBuilder.Environment.ContentRootPath}/{configurationBuilder.Configuration["FFMpeg:BaseVideoPath"]}";
            });
        services.AddFusionCache()
            .WithSerializer(
                new FusionCacheNewtonsoftJsonSerializer());
        return services;
    }
}