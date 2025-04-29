using IntelligentMonitoringSystem.Domain.AccessDevices;
using IntelligentMonitoringSystem.Domain.LeaveApplications;
using IntelligentMonitoringSystem.Domain.MessageCenters;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;
using IntelligentMonitoringSystem.Domain.Shared;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentMonitoringSystem.Domain;

/// <summary>
///     领域模块
/// </summary>
public static class DomainModule
{
    /// <summary>
    ///     添加领域服务
    /// </summary>
    /// <param name="services">services</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddDomain(
        this IServiceCollection services)
    {
        services.AddDomainSharedModule();
        services.AddTransient<IPersonnelAccessRecordManage, PersonnelAccessRecordManage>();
        services.AddSingleton<IDomainEventsDispatcher, DomainEventsDispatcher>();
        services.AddSingleton<IMessageCenterManage, MessageCenterManage>();
        services.AddTransient<ILeaveApplicationManage, LeaveApplicationManage>();
        services.AddTransient<IAccessDeviceManage, AccessDeviceManage>();
        return services;
    }
}