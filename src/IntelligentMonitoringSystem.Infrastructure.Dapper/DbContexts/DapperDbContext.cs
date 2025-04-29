using IntelligentMonitoringSystem.Domain.AccessDevices;
using IntelligentMonitoringSystem.Domain.EventSources;
using IntelligentMonitoringSystem.Domain.LeaveApplications;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;
using IntelligentMonitoringSystem.Infrastructure.Common.Data;

namespace IntelligentMonitoringSystem.Infrastructure.Dapper.DbContexts;

/// <summary>
///     DapperDbContext
/// </summary>
[ConnectionStringName("Default")]
public class DapperDbContext : IDapperDbContext
{
    /// <summary>
    ///     LeaveApplications
    /// </summary>
    public DapperDbSet<LeaveApplication> LeaveApplications { get; set; }

    /// <summary>
    ///     AccessDevices
    /// </summary>
    public DapperDbSet<AccessDevice> AccessDevices { get; set; }

    /// <summary>
    ///     AbnormalPersonnelAccessRecords
    /// </summary>
    public DapperDbSet<AbnormalPersonnelAccessRecord> AbnormalPersonnelAccessRecords { get; set; }

    /// <summary>
    ///     PersonnelAccessRecords
    /// </summary>
    public DapperDbSet<PersonnelAccessRecord> PersonnelAccessRecords { get; set; }

    /// <summary>
    ///     PersonnelAccessRecordStatistics
    /// </summary>
    public DapperDbSet<PersonnelAccessRecordStatistics> PersonnelAccessRecordStatistics { get; set; }

    /// <summary>
    ///     AbnormalPersonnelAccessRecordStatistics
    /// </summary>
    public DapperDbSet<AbnormalPersonnelAccessRecordStatistics> AbnormalPersonnelAccessRecordStatistics { get; set; }

    /// <summary>
    ///     EventStreams
    /// </summary>
    public DapperDbSet<EventStream> EventStreams { get; set; }
}