using Ardalis.SmartEnum.JsonNet;
using IntelligentMonitoringSystem.Application.Shared.Dto.Lists;
using IntelligentMonitoringSystem.Domain.Shared.AccessDevices.Enums;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.Devices.Dtos;

/// <summary>
///     监控中心设备列表
/// </summary>
public class MonitorCenterDeviceDto
{
    /// <summary>
    ///     总数量
    /// </summary>
    [JsonProperty("totalCount")]
    public int TotalCount { get; set; }

    /// <summary>
    ///     在线数量
    /// </summary>
    [JsonProperty("liveCount")]
    public int LiveCount { get; set; }

    /// <summary>
    ///     最后更新时间
    /// </summary>
    [JsonProperty("lastUpdateTime")]
    public DateTime? LastUpdateTime { get; set; }

    /// <summary>
    ///     设备类型
    /// </summary>
    [JsonProperty("list")]
    public ListResultDto<DeviceInfoDto> List { get; set; } = new();
}

/// <summary>
///     设备列表
/// </summary>
public class DeviceInfoDto
{
    /// <summary>
    ///     设备Id
    /// </summary>
    [JsonProperty("deviceId")]
    public string DeviceId { get; set; }

    /// <summary>
    ///     设备名称
    /// </summary>
    [JsonProperty("deviceName")]
    public string DeviceName { get; set; }

    /// <summary>
    ///     摄像头型号名称
    /// </summary>
    [JsonProperty("camModelName")]
    public string CamModelName { get; set; }

    /// <summary>
    ///     设备代表方向：0出，1入
    /// </summary>
    [JsonProperty("accessDeviceType")]
    [JsonConverter(typeof(SmartEnumNameConverter<AccessDeviceTypeSmartEnum, int>))]
    public AccessDeviceTypeSmartEnum AccessDeviceType { get; set; }

    /// <summary>
    ///     在线状态：0离线，1在线
    /// </summary>
    [JsonProperty("deviceStatus")]
    public int DeviceStatus { get; set; }
}