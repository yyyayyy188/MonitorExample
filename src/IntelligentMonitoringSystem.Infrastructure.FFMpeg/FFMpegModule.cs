using FFMpegCore;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;
using IntelligentMonitoringSystem.Infrastructure.FFMpeg.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentMonitoringSystem.Infrastructure.FFMpeg;

/// <summary>
///     FFMpeg模块.
/// </summary>
public static class FFMpegModule
{
    /// <summary>
    ///     添加基础服务.
    /// </summary>
    /// <param name="services">services.</param>
    /// <param name="configuration">执行文件地址</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddFFMpegModule(
        this IServiceCollection services, IConfiguration configuration)
    {
        GlobalFFOptions.Configure(new FFOptions { BinaryFolder = configuration["FFMpeg:BinaryFolder"] ?? string.Empty });
        services.AddTransient<ITransformVideoRepository, TransformVideoRepository>();
        return services;
    }
}