using IntelligentMonitoringSystem.Application.Common.Contracts;

namespace IntelligentMonitoringSystem.Application.AlarmEventMessages.Create;

/// <summary>
///     报警事件消息
/// </summary>
public class AlarmEventMessageCommand : CommandBase
{
    /// <summary>
    ///     应用ID
    /// </summary>
    public string AppId { get; set; } = null!;

    /// <summary>
    ///     sig
    /// </summary>
    public string Sig { get; set; } = null!;

    /// <summary>
    ///     报警事件类型
    /// </summary>
    public string MsgType { get; set; } = null!;

    /// <summary>
    ///     时间戳
    /// </summary>
    public DateTime TimeStamp { get; set; }

    /// <summary>
    ///     设备ID
    /// </summary>
    public string DeviceId { get; set; } = null!;

    /// <summary>
    ///     检测时间
    /// </summary>
    public DateTime DetectTime { get; set; }

    /// <summary>
    ///     原始报警内容
    /// </summary>
    public string OriginalAlarmContext { get; set; } = null!;

    /// <summary>
    ///     报警内容信息
    /// </summary>
    public AlarmContext AlarmContext { get; set; }

    /// <summary>
    ///     设备名称
    /// </summary>
    public string DeviceName { get; set; }

    /// <summary>
    ///     设备地址
    /// </summary>
    public string DeviceAddress { get; set; }

    /// <summary>
    ///     区域名称
    /// </summary>
    public string RegionName { get; set; }

    /// <summary>
    ///     店铺名称
    /// </summary>
    public string StoreName { get; set; }

    /// <summary>
    ///     店铺Id
    /// </summary>
    public string StoreId { get; set; }

    /// <summary>
    ///     区域
    /// </summary>
    public string Region { get; set; }
}

/// <summary>
///     报警事件消息体
/// </summary>
public class AlarmContext
{
    /// <summary>
    ///     面容Id
    /// </summary>
    public string? FaceId { get; set; }

    /// <summary>
    ///     性别
    /// </summary>
    public string? Gender { get; set; }

    /// <summary>
    ///     年龄
    /// </summary>
    public short Age { get; set; }

    /// <summary>
    ///     采集时间
    /// </summary>
    public DateTime SnapTime { get; set; }

    /// <summary>
    ///     采集连接
    /// </summary>
    public string? SnapUrl { get; set; }

    /// <summary>
    ///     姓名
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     面容地址
    /// </summary>
    public string? FaceUrl { get; set; }

    /// <summary>
    ///     人脸识别图片
    /// </summary>
    public string RecognitionFaceOssUrl { get; set; }

    /// <summary>
    ///     人脸对象Id
    /// </summary>
    public string FaceObjectId { get; set; }
}