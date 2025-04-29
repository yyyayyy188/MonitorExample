using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.AlarmEventMessages.Dtos;

/// <summary>
///     报警事件消息请求
/// </summary>
public class AlarmEventMessageRequest
{
    /// <summary>
    ///     应用ID
    /// </summary>
    [JsonProperty("appId")]
    public string AppId { get; set; } = null!;

    /// <summary>
    ///     sig
    /// </summary>
    [JsonProperty("sig")]
    public string Sig { get; set; } = null!;

    /// <summary>
    ///     应用ID
    /// </summary>
    [JsonProperty("msgList")]
    public List<AlarmEventMessage> MsgList { get; set; } = [];
}

/// <summary>
///     报警事件消息体
/// </summary>
public class AlarmEventMessage
{
    /// <summary>
    ///     报警事件类型
    /// </summary>
    [JsonProperty("msgType")]
    public string MsgType { get; set; } = null!;

    /// <summary>
    ///     时间戳
    /// </summary>
    [JsonProperty("timeStamp")]
    public string TimeStamp { get; set; } = null!;

    /// <summary>
    ///     报警事件数据
    /// </summary>
    [JsonProperty("data")]
    public AlarmEventMessageData? Data { get; set; }
}

/// <summary>
///     报警事件数据
/// </summary>
public class AlarmEventMessageData
{
    /// <summary>
    ///     报警内容
    /// </summary>
    [JsonProperty("alarmContext")]
    public string AlarmContext { get; set; } = null!;

    /// <summary>
    ///     设备ID
    /// </summary>
    [JsonProperty("deviceId")]
    public string DeviceId { get; set; } = null!;

    /// <summary>
    ///     设备名称
    /// </summary>
    [JsonProperty("deviceName")]
    public string DeviceName { get; set; } = null!;

    /// <summary>
    ///     检测时间
    /// </summary>
    [JsonProperty("detectTime")]
    public string DetectTime { get; set; } = null!;

    /// <summary>
    ///     设备地址
    /// </summary>
    [JsonProperty("deviceAddress")]
    public string? DeviceAddress { get; set; }

    /// <summary>
    ///     区域名称
    /// </summary>
    [JsonProperty("regionName")]
    public string? RegionName { get; set; }

    /// <summary>
    ///     店铺名称
    /// </summary>
    [JsonProperty("storeName")]
    public string? StoreName { get; set; }

    /// <summary>
    ///     店铺Id
    /// </summary>
    [JsonProperty("storeId")]
    public string? StoreId { get; set; }

    /// <summary>
    ///     区域
    /// </summary>
    [JsonProperty("region")]
    public string? Region { get; set; }
}

/// <summary>
///     报警上下文
/// </summary>
public class AlarmContextBody
{
    /// <summary>
    ///     相似度
    /// </summary>
    [JsonProperty("similarity")]
    public string Similarity { get; set; }

    /// <summary>
    ///     面容Id
    /// </summary>
    [JsonProperty("faceId")]
    public string? FaceId { get; set; }

    /// <summary>
    ///     性别
    /// </summary>
    [JsonProperty("gender")]
    public string? Gender { get; set; }

    /// <summary>
    ///     年龄
    /// </summary>
    [JsonProperty("age")]
    public short Age { get; set; }

    /// <summary>
    ///     采集时间
    /// </summary>
    [JsonProperty("snapTime")]
    public long SnapTime { get; set; }

    /// <summary>
    ///     采集连接
    /// </summary>
    [JsonProperty("snapUrl")]
    public string SnapUrl { get; set; }

    /// <summary>
    ///     姓名
    /// </summary>
    [JsonProperty("realName")]
    public string? RealName { get; set; }

    /// <summary>
    ///     抓拍对象
    /// </summary>
    [JsonProperty("snapObjectId")]
    public string SnapObjectId { get; set; }

    /// <summary>
    ///     抓拍对象
    /// </summary>
    [JsonProperty("snapImageId")]
    public string SnapImageId { get; set; }

    /// <summary>
    ///     面容地址
    /// </summary>
    [JsonProperty("faceUrl")]
    public string? FaceUrl { get; set; }

    /// <summary>
    ///     监控任务名称
    /// </summary>
    [JsonProperty("monitorTaskName")]
    public string MonitorTaskName { get; set; }

    /// <summary>
    ///     面容对象Id
    /// </summary>
    [JsonProperty("faceObjectId")]
    public string FaceObjectId { get; set; }

    /// <summary>
    ///     人脸识别图片地址
    /// </summary>
    [JsonProperty("recognitionFaceOssUrl")]
    public string RecognitionFaceOssUrl { get; set; }
}