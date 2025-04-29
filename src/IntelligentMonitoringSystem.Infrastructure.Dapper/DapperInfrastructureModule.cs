using Ardalis.SmartEnum.Dapper;
using Dapper;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.AccessDevices.Enums;
using IntelligentMonitoringSystem.Domain.Shared.LeaveApplications.Enums;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using IntelligentMonitoringSystem.Domain.Users;
using IntelligentMonitoringSystem.Infrastructure.Dapper.DbContexts;
using IntelligentMonitoringSystem.Infrastructure.Dapper.Repositories.PersonnelAccessRecords;
using IntelligentMonitoringSystem.Infrastructure.Dapper.Repositories.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentMonitoringSystem.Infrastructure.Dapper;

/// <summary>
///     Infrastructure module
/// </summary>
public static class DapperInfrastructureModule
{
    /// <summary>
    ///     Adds Dapper infrastructure
    /// </summary>
    /// <param name="services">services</param>
    /// <param name="configuration">configuration</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddDapperInfrastructure<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration) where TDbContext : IDapperDbContext
    {
        SqlMapper.AddTypeHandler(new SmartEnumByValueTypeHandler<LeaveApplicationStatusSmartEnum>());
        SqlMapper.AddTypeHandler(new SmartEnumByValueTypeHandler<AccessTypeSmartEnum, string>());
        SqlMapper.AddTypeHandler(new SmartEnumByValueTypeHandler<AccessDeviceTypeSmartEnum>());
        SqlMapper.AddTypeHandler(new SmartEnumByValueTypeHandler<AccessStatusSmartEnum, string>());
        SqlMapper.AddTypeHandler(new SmartEnumByValueTypeHandler<AbnormalPersonnelAccessRecordStatusSmartEnum, string>());
        SqlMapper.AddTypeHandler(new SmartEnumByValueTypeHandler<LeaveStatusSmartEnum, string>());
        services.AddDapperDbContextAndDefaultRepository<TDbContext>(configuration);
        services.AddTransient<IAbnormalPersonnelAccessRecordRepository, AbnormalPersonnelAccessRecordRepository>();
        services.AddTransient<IUserRepository, UserRepository>();

        return services;
    }
}