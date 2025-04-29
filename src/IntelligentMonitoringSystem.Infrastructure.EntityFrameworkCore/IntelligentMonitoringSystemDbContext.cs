using IntelligentMonitoringSystem.Domain.AccessDevices;
using IntelligentMonitoringSystem.Domain.EventSources;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using IntelligentMonitoringSystem.Infrastructure.Common.Data;
using Microsoft.EntityFrameworkCore;
using SmartEnum.EFCore;

namespace IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore;

/// <summary>
///     IntelligentMonitoringSystemDbContext
/// </summary>
/// <param name="options">options.</param>
[ConnectionStringName("Default")]
public class IntelligentMonitoringSystemDbContext(DbContextOptions options, IDomainEventsDispatcher domainEventsDispatcher)
    : BaseDbContext(options)
{
    /// <summary>
    ///     TopicAlarmEventLogs
    /// </summary>
    public DbSet<TopicAlarmEventLog> TopicAlarmEventLogs { get; set; }

    /// <summary>
    ///     PersonnelAccessRecords
    /// </summary>
    public DbSet<PersonnelAccessRecord> PersonnelAccessRecords { get; set; }

    /// <summary>
    ///     AbnormalPersonnelAccessRecords
    /// </summary>
    public DbSet<AbnormalPersonnelAccessRecord> AbnormalPersonnelAccessRecords { get; set; }

    /// <summary>
    ///     EventStreams
    /// </summary>
    public DbSet<EventStream> EventStreams { get; set; }

    /// <summary>
    ///     AccessDevices
    /// </summary>
    public DbSet<AccessDevice> AccessDevices { get; set; }

    /// <summary>
    ///     Save changes and dispatch events
    /// </summary>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>int.</returns>
    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var entitiesWithEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(e => e.Entity)
            .ToArray();
        var immediateProcessingList = new List<IDomainEvent>();
        foreach (var domain in entitiesWithEvents)
        {
            var domainEvents = domain.GetEvents().ToArray();
            if (domainEvents.Length == 0)
            {
                continue;
            }

            domain.ClearEvents();
            var events = domainEvents.OrderByDescending(x => x.EventOrder)
                .Select(x => new { x.EventData, x.IsImmediateProcessing }).ToList();
            if (events.Count == 0)
            {
                continue;
            }

            // 处理需要立即处理的事件
            immediateProcessingList.AddRange(events.Where(x => x.IsImmediateProcessing ?? false)
                .Select(x => x.EventData));
            await EventStreams.AddRangeAsync(
                events.Select(x => new EventStream(x.EventData.StreamId, x.EventData, x.IsImmediateProcessing ?? false)),
                cancellationToken);
        }

        var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (immediateProcessingList.Count != 0)
        {
            await domainEventsDispatcher.DispatchImmediateEvents(immediateProcessingList);
        }

        return result;
    }

    /// <summary>
    ///     Configure entity
    /// </summary>
    /// <param name="modelBuilder">modelBuilder</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ConfigureSmartEnum();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityFrameWorkCoreEntityTypeConfiguration).Assembly);
    }
}