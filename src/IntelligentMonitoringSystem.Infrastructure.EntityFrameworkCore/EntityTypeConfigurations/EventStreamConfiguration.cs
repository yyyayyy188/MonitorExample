using IntelligentMonitoringSystem.Domain.EventSources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore.EntityTypeConfigurations;

/// <summary>
///     There are no comments for EventStreamStoreConfiguration in the schema.
/// </summary>
public class EventStreamConfiguration : IEntityTypeConfiguration<EventStream>
{
    /// <summary>
    ///     There are no comments for Configure(EntityTypeBuilder<EventStreamStore /> builder) method in the schema.
    /// </summary>
    /// <param name="builder">builder</param>
    public void Configure(EntityTypeBuilder<EventStream> builder)
    {
        builder.ToTable(@"event_stream", @"intelligent_monitoring_system");
        builder.Property(x => x.Id).HasColumnName(@"id").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Type).HasColumnName(@"type").HasColumnType(@"varchar");
        builder.Property(x => x.FullTypeName).HasColumnName(@"full_type_name").HasColumnType(@"varchar");
        builder.Property(x => x.Status).HasColumnName(@"status").HasColumnType(@"int")
            .HasDefaultValueSql(@"0");
        builder.Property(x => x.StreamId).HasColumnName(@"stream_id").HasColumnType(@"varchar");
        builder.Property(x => x.StreamPosition).HasColumnName(@"stream_position").HasColumnType(@"int")
            .HasDefaultValueSql(@"1");
        builder.Property(x => x.ProcessCount).HasColumnName(@"process_count").HasColumnType(@"int")
            .HasDefaultValueSql(@"0");
        builder.Property(x => x.CreateTime).HasColumnName(@"create_time").HasColumnType(@"datetime").IsRequired()
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql(@"CURRENT_TIMESTAMP");
        builder.Property(x => x.LastProcessTime).HasColumnName(@"last_process_time").HasColumnType(@"datetime");
        builder.Property(x => x.Message).HasColumnName(@"message").HasColumnType(@"varchar");
        builder.Property(x => x.Data).HasColumnName(@"data").HasColumnType(@"varchar");
        builder.HasKey(@"Id");
    }
}