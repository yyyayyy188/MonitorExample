using IntelligentMonitoringSystem.Application.Common.Contracts;

namespace IntelligentMonitoringSystem.Application.Devices.LiveStreamingThumbnail;

/// <summary>
///     获取设备直播截图
/// </summary>
public class LiveStreamingThumbnailQuery : IQuery<Dictionary<string, string?>>;