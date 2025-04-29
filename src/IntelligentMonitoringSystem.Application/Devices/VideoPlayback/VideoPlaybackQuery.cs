using IntelligentMonitoringSystem.Application.Common.Contracts;

namespace IntelligentMonitoringSystem.Application.Devices.VideoPlayback;

/// <summary>
///     视频回放
/// </summary>
public class VideoPlaybackQuery : IQuery<string>
{
    /// <summary>
    ///     开始时间
    /// </summary>
    public DateTime BeginTime { get; set; }

    /// <summary>
    ///     结束时间
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    ///     设备ID
    /// </summary>
    public string DeviceId { get; set; }
}