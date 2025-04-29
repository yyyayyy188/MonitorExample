// <copyright file="ReferenceInfrastructureModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using IntelligentMonitoringSystem.Domain.AccessDevices.Repositories;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.Configs;
using IntelligentMonitoringSystem.Infrastructure.Reference.Apis.DownLoads;
using IntelligentMonitoringSystem.Infrastructure.Reference.Apis.FileServers;
using IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos;
using IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos.DelegatingHandlers;
using IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos.Repositories;
using IntelligentMonitoringSystem.Infrastructure.Reference.PersonnelAccessRecords.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;

namespace IntelligentMonitoringSystem.Infrastructure.Reference;

/// <summary>
///     引用模块.
/// </summary>
public static class ReferenceInfrastructureModule
{
    /// <summary>
    ///     添加基础服务.
    /// </summary>
    /// <param name="services">services.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddReferenceInfrastructure(
        this IServiceCollection services)
    {
        services.AddTransient<DynamicUrlDelegatingHandler>();
        services.AddTransient<VideoAuthenticatedHttpClientHandler>();
        var options = services.BuildServiceProvider().GetService<IOptions<IntelligentMonitorSystemHttpClientConfig>>();
        if (options?.Value == null)
        {
            throw new AggregateException("not found api config");
        }

        var settings = new RefitSettings(new NewtonsoftJsonContentSerializer());

        // 注册服务
        services.AddRefitClient<IDownLoadFileApi>(settings)
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(options.Value.GetTeamHost("VideIot"));
                c.Timeout = TimeSpan.FromSeconds(10);
            })
            .AddHttpMessageHandler<DynamicUrlDelegatingHandler>()
            ;

        services.AddRefitClient<IFileServerApi>(settings)
            .ConfigureHttpClient(c =>
                c.BaseAddress = new Uri(options.Value.GetTeamHost("FileServer")))
            ;

        services.AddRefitClient<IVideoApi>(settings)
            .ConfigureHttpClient(c =>
                c.BaseAddress = new Uri(options.Value.GetTeamHost("OpenVideo")))
            .AddHttpMessageHandler<VideoAuthenticatedHttpClientHandler>()
            ;

        services.AddSingleton<IPersonnelAccessRecordFileRepository, PersonnelAccessRecordFileRepository>();
        services.AddSingleton<IPersonnelAccessRecordVideoRepository, PersonnelAccessRecordVideoRepository>();
        services.AddSingleton<IAccessDeviceVideoRepository, AccessDeviceVideoRepository>();
        return services;
    }
}