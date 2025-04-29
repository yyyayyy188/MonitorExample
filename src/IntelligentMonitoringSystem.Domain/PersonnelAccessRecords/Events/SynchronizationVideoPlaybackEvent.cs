using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Events;

/// <summary>
///     视频回放同步事件
/// </summary>
public class SynchronizationVideoPlaybackEvent : DomainEventBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SynchronizationVideoPlaybackEvent" /> class.
    ///     视频回放同步事件
    /// </summary>
    public SynchronizationVideoPlaybackEvent()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="SynchronizationVideoPlaybackEvent" /> class.
    /// </summary>
    /// <param name="eventIdentifier">事件流Id</param>
    /// <param name="deviceId">设备Id</param>
    /// <param name="accessTime">访问时间</param>
    public SynchronizationVideoPlaybackEvent(Guid eventIdentifier, string deviceId, DateTime accessTime)
    {
        EventIdentifier = eventIdentifier;
        DeviceId = deviceId;
        AccessTime = accessTime;
    }

    /// <summary>
    ///     获取事件流Id
    /// </summary>
    public override string StreamId => EventIdentifier.ToString();

    /// <summary>
    ///     人员出入记录事件Id
    /// </summary>
    public Guid EventIdentifier { get; set; }

    /// <summary>
    ///     设备Id
    /// </summary>
    public string DeviceId { get; set; }

    /// <summary>
    ///     访问时间
    /// </summary>
    public DateTime AccessTime { get; set; }
}