using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;

namespace IntelligentMonitoringSystem.Infrastructure.Dapper.Repositories.PersonnelAccessRecords.Po;

/// <summary>
///     告警记录
/// </summary>
public class AbnormalPersonnelAccessRecordPo
{
    /// <summary>
    ///     告警记录id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     访问记录id
    /// </summary>
    public int PersonnelAccessRecordId { get; set; }

    /// <summary>
    ///     离开时间
    /// </summary>
    public DateTime LeaveTime { get; set; }

    /// <summary>
    ///     回归时间
    /// </summary>
    public DateTime? ReturnTime { get; set; }

    /// <summary>
    ///     下次告警时间
    /// </summary>
    public DateTime NextWaringTime { get; set; }

    /// <summary>
    ///     状态
    /// </summary>
    public AbnormalPersonnelAccessRecordStatusSmartEnum Status { get; set; }

    /// <summary>
    ///     人脸id
    /// </summary>
    public string FaceId { get; set; }

    /// <summary>
    ///     设备id
    /// </summary>
    public string DeviceId { get; set; }

    /// <summary>
    ///     设备名称
    /// </summary>
    public string DeviceName { get; set; }

    /// <summary>
    ///     姓名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     年龄
    /// </summary>
    public short Age { get; set; }

    /// <summary>
    ///     性别
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    ///     访问类型
    /// </summary>
    public AccessTypeSmartEnum AccessType { get; set; }

    /// <summary>
    ///     访问状态
    /// </summary>
    public AccessStatusSmartEnum AccessStatus { get; set; }

    /// <summary>
    ///     访问时间
    /// </summary>
    public DateTime AccessTime { get; set; }

    /// <summary>
    ///     人脸图片
    /// </summary>
    public string OriginalFaceUrl { get; set; }

    /// <summary>
    ///     抓拍图片
    /// </summary>
    public string OriginalSnapUrl { get; set; }

    /// <summary>
    ///     人脸识别图片
    /// </summary>
    public string RecognitionFaceOssUrl { get; set; }

    /// <summary>
    ///     抓拍图片
    /// </summary>
    public string SnapImg { get; set; }

    /// <summary>
    ///     人脸图片
    /// </summary>
    public string FaceImg { get; set; }

    /// <summary>
    ///     人脸识别路径
    /// </summary>
    public string RecognitionFacePath { get; set; }

    /// <summary>
    ///     备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    ///     离开状态
    /// </summary>
    public LeaveStatusSmartEnum? LeaveStatus { get; set; }

    /// <summary>
    ///     离开申请id
    /// </summary>
    public int? LeaveApplicationId { get; set; }

    /// <summary>
    ///     预计回归时间
    /// </summary>
    public DateTime? ExpectedReturnTime { get; set; }

    /// <summary>
    ///     访问记录事件标识符
    /// </summary>
    public Guid PersonnelAccessRecordEventIdentifier { get; set; }

    /// <summary>
    ///     护理人员
    /// </summary>
    public string Nurse { get; set; }

    /// <summary>
    ///     房间号
    /// </summary>
    public string RoomNo { get; set; }

    /// <summary>
    ///     设备回放
    /// </summary>
    public string DevicePlayback { get; set; }
}