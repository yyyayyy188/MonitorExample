// <copyright file="WebApiModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Reflection;
using Coravel;
using IntelligentMonitoringSystem.Application;
using IntelligentMonitoringSystem.Application.Shared;
using IntelligentMonitoringSystem.Domain.Shared;
using IntelligentMonitoringSystem.Domain.Shared.Configs;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Configs;
using IntelligentMonitoringSystem.WebApi.BackgroundServices;
using IntelligentMonitoringSystem.WebApi.Extensions;
using IntelligentMonitoringSystem.WebApi.Infrastructures.ExceptionHandlers;
using IntelligentMonitoringSystem.WebApi.Invocable.EventSources;
using IntelligentMonitoringSystem.WebApi.Invocable.PersonnelAccessRecords;
using IntelligentMonitoringSystem.WebApi.Validators;
using IntelligentMonitoringSystem.WebApi.WebSockets;
using IntelligentMonitoringSystem.WebApi.WebSockets.Manages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using StackExchange.Profiling;
using StackExchange.Profiling.SqlFormatters;
using StackExchange.Profiling.Storage;
using StackExchange.Redis;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.NewtonsoftJson;

namespace IntelligentMonitoringSystem.WebApi;

/// <summary>
///     WebApi Module.
/// </summary>
public static class WebApiModule
{
    /// <summary>
    ///     Add WebApi.
    /// </summary>
    /// <param name="services">services.</param>
    /// <param name="configurationBuilder">configurationBuilder.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddWebApi(
        this IServiceCollection services,
        IHostApplicationBuilder configurationBuilder)
    {
        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var conn = configurationBuilder.Configuration["Redis:ConnectionString"] ?? string.Empty;
            return ConnectionMultiplexer.Connect(conn);
        });

        services.AddHostedService<AlarmEventBackgroundService>();
        services.AddHostedService<MessageCenterBackGroundService>();
        services.AddHostedService<PersonnelAccessRecordDelayMessageBackgroundService>();
        services.Configure<HostOptions>(hostOptions =>
        {
            hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
        });

        services.AddSingleton<ConnectionManager>();
        services.AddSingleton<WebSocketHandler>();
        services.AddScoped(typeof(ValidationFilter<>));
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        });
        services.AddEndpointsApiExplorer();
        if (!configurationBuilder.Environment.IsProduction())
        {
            services.AddMiniProfiler(options =>
            {
                options.RouteBasePath = $"{CleanSvcRouteAttribute.ExternalRouteBase}/profiler";
                options.SqlFormatter = new SqlServerFormatter();
                ((MemoryCacheStorage)options.Storage).CacheDuration = TimeSpan.FromMinutes(10);

                // Optionally disable "Connection Open()", "Connection Close()" (and async variants).
                options.TrackConnectionOpenClose = true;

                // Optionally use something other than the "light" color scheme.
                options.ColorScheme = ColorScheme.Dark;

                // Optionally change the number of decimal places shown for millisecond timings.
                options.PopupDecimalPlaces = 2;

                // Enabled sending the Server-Timing header on responses
                options.EnableServerTimingHeader = true;

                // Optionally disable MVC filter profiling
                options.EnableMvcFilterProfiling = false;

                // Optionally disable MVC view profiling
                options.EnableMvcViewProfiling = false;
                options.MaxUnviewedProfiles = 2;
                options.IgnoredPaths.Add("/lib");
                options.IgnoredPaths.Add("/css");
                options.IgnoredPaths.Add("/js");
                options.IgnoredPaths.Add("/api/monitoring-svc/web-socket");
                options.IgnoredPaths.Add("/api/monitoring-svc/swagger");
            }).AddEntityFramework();
        }

        if (configurationBuilder.Environment.IsDevelopment())
        {
            services.AddSwaggerGen(options =>
            {
                var xmlFileList = new List<string>
                {
                    Path.Combine(AppContext.BaseDirectory, $"{typeof(WebApiModule).Assembly.GetName().Name}.xml"),
                    Path.Combine(AppContext.BaseDirectory, $"{typeof(ApplicationModule).Assembly.GetName().Name}.xml"),
                    Path.Combine(
                        AppContext.BaseDirectory,
                        $"{typeof(ApplicationSharedModule).Assembly.GetName().Name}.xml")
                };
                foreach (var xmlPath in xmlFileList)
                {
                    options.IncludeXmlComments(xmlPath);
                }

                options.DocumentFilter<SmartEnumDocumentFilter>(new List<Assembly> { typeof(DomainSharedModule).Assembly });
            });
        }

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = false;
        });

        services.AddIntelligentMonitoringSystemFluentValidation();
        services.AddProblemDetails();
        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddHealthChecks();

        services.Configure<PersonnelAccessRecordConfig>(
            configurationBuilder.Configuration.GetSection(PersonnelAccessRecordConfig.PositionOptions));

        services.Configure<IntelligentMonitorSystemHttpClientConfig>(
            configurationBuilder.Configuration.GetSection(IntelligentMonitorSystemHttpClientConfig.PositionOptions));

        services.AddScheduler();
        services.AddTransient<AbnormalPersonnelAccessRecordInvocable>();
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