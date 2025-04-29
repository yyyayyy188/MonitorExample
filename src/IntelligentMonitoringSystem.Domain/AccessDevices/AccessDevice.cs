using IntelligentMonitoringSystem.Domain.Shared.AccessDevices.Enums;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;

namespace IntelligentMonitoringSystem.Domain.AccessDevices;

/// <summary>
///     监控设备信息
/// </summary>
public class AccessDevice : AggregateRoot
{
    /// <summary>
    ///     设备Id
    /// </summary>
    public string DeviceId { get; set; }

    /// <summary>
    ///     设备名称
    /// </summary>
    public string DeviceName { get; set; }

    /// <summary>
    ///     摄像头型号名称
    /// </summary>
    public string CamModelName { get; set; }

    /// <summary>
    ///     在线状态：0离线，1在线
    /// </summary>
    public int DeviceStatus { get; set; }

    /// <summary>
    ///     开关状态：0关闭，1打开
    /// </summary>
    public int DeviceSwitch { get; set; }

    /// <summary>
    ///     更新时间
    /// </summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>
    ///     设备代表方向：0出，1入
    /// </summary>
    public AccessDeviceTypeSmartEnum AccessDeviceType { get; set; }

    /// <summary>
    ///     视频直播流截图地址
    /// </summary>
    public string VideoLiveStreamingThumbnail { get; set; }
}