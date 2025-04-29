using IntelligentMonitoringSystem.Domain.AccessDevices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore.EntityTypeConfigurations;

/// <summary>
///     AccessDeviceConfiguration
/// </summary>
public class AccessDeviceConfiguration
{
    /// <summary>
    ///     There are no comments for TDeviceInfoConfiguration in the schema.
    /// </summary>
    public class TDeviceInfoConfiguration : IEntityTypeConfiguration<AccessDevice>
    {
        /// <summary>
        ///     There are no comments for Configure(EntityTypeBuilder<AccessDevice /> builder) method in the schema.
        /// </summary>
        /// <param name="builder">builder</param>
        public void Configure(EntityTypeBuilder<AccessDevice> builder)
        {
            builder.ToTable(@"t_device_info", @"intelligent_monitoring_system");
            builder.Property(x => x.CamModelName).HasColumnName(@"cam_model_name").HasColumnType(@"varchar").IsRequired()
                .HasMaxLength(64);
            builder.Property(x => x.DeviceId).HasColumnName(@"device_id").HasColumnType(@"varchar").IsRequired()
                .HasMaxLength(64);
            builder.Property(x => x.DeviceName).HasColumnName(@"device_name").HasColumnType(@"varchar").IsRequired()
                .HasMaxLength(64);
            builder.Property(x => x.DeviceStatus).HasColumnName(@"device_status").HasColumnType(@"int").IsRequired();
            builder.Property(x => x.DeviceSwitch).HasColumnName(@"device_switch").HasColumnType(@"int").IsRequired();
            builder.Property(x => x.AccessDeviceType).HasColumnName(@"device_direction").HasColumnType(@"int");
            builder.Property(x => x.UpdateTime).HasColumnName(@"update_time");
            builder.Ignore(x => x.VideoLiveStreamingThumbnail);
            builder.HasKey(@"DeviceId");
        }
    }
}