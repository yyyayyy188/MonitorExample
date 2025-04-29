using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore.EntityTypeConfigurations;

/// <summary>
///     There are no comments for AbnormalPersonnelAccessRecordConfiguration in the schema.
/// </summary>
public class AbnormalPersonnelAccessRecordConfiguration : IEntityTypeConfiguration<AbnormalPersonnelAccessRecord>
{
    /// <summary>
    ///     There are no comments for Configure(EntityTypeBuilder{AbnormalPersonnelAccessRecord} builder) method in the schema.
    /// </summary>
    /// <param name="builder">builder</param>
    public void Configure(EntityTypeBuilder<AbnormalPersonnelAccessRecord> builder)
    {
        builder.ToTable(@"abnormal_personnel_access_record");
        builder.Property(x => x.Id).HasColumnName(@"id").HasColumnType(@"int").IsRequired();
        builder.Property(x => x.FaceId).HasColumnName(@"face_id").HasColumnType(@"varchar(64)").IsRequired();
        builder.Property(x => x.PersonnelAccessRecordId).HasColumnName(@"personnel_access_record_id")
            .HasColumnType(@"int").IsRequired();
        builder.Property(x => x.LeaveTime).HasColumnName(@"leave_time").IsRequired();
        builder.Property(x => x.ReturnTime).HasColumnName(@"return_time");
        builder.Property(x => x.NextWaringTime).HasColumnName(@"next_waring_time").IsRequired();
        builder.Property(x => x.Status).HasColumnName(@"status").HasColumnType(@"char(1)").IsRequired();
        builder.Property(x => x.CreateTime).HasColumnName(@"create_time").IsRequired();
        builder.Property(x => x.LastEditTime).HasColumnName(@"last_edit_time");
        builder.Property(x => x.EventIdentifier).HasColumnName(@"event_identifier").HasColumnType(@"char(36)");
        builder.Ignore(x => x.PersonnelAccessRecord);
        builder.HasKey(@"Id");
        builder.HasIndex(@"EventIdentifier").IsUnique().HasDatabaseName(@"uk_apar_event_identifier");
    }
}