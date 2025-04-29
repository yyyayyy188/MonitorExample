using IntelligentMonitoringSystem.Application.Common.Contracts;

namespace IntelligentMonitoringSystem.Application.Devices.LiveStreaming;

/// <summary>
///     直播流查询
/// </summary>
public class LiveStreamingQuery : IQuery<string>
{
    /// <summary>
    ///     设备ID
    /// </summary>
    public string DeviceId { get; set; }
}