using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;

namespace IntelligentMonitoringSystem.Domain.Users;

/// <summary>
///     用户账号
/// </summary>
public class UserAccount : AggregateRoot
{
    /// <summary>
    ///     唯一主键ID
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    ///     创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    ///     邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     紧急联系人
    /// </summary>
    public string EmergencyContact { get; set; }

    /// <summary>
    ///     紧急联系人电话
    /// </summary>
    public string EmergencyContactPhone { get; set; }

    /// <summary>
    ///     紧急联系人关系
    /// </summary>
    public string EmergencyContactRelation { get; set; }

    /// <summary>
    ///     是否是管理员：0否，1是
    /// </summary>
    public int? IsAdmin { get; set; }

    /// <summary>
    ///     是否为首次登录：0首次，1非首次
    /// </summary>
    public int? IsFirstLogin { get; set; }

    /// <summary>
    ///     账号密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    ///     联系方式
    /// </summary>
    public string Telephone { get; set; }

    /// <summary>
    ///     账号名称
    /// </summary>
    public string UserAccountName { get; set; }

    /// <summary>
    ///     用户姓名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    ///     千里眼平台对应人脸ID
    /// </summary>
    public string FaceId { get; set; }

    /// <summary>
    ///     护理人员
    /// </summary>
    public string Nurse { get; set; }

    /// <summary>
    ///     房间号
    /// </summary>
    public string RoomNo { get; set; }

    /// <summary>
    ///     最近一次进入时间
    /// </summary>
    public DateTime? LatestEnter { get; set; }

    /// <summary>
    ///     最近一次离开时间
    /// </summary>
    public DateTime? LatestExit { get; set; }

    /// <summary>
    ///     房间状态
    /// </summary>
    public int RoomStatus { get; set; }

    /// <summary>
    ///     平均时长
    /// </summary>
    public double AverageInterval { get; set; } = 0;

    /// <summary>
    ///     外出次数
    /// </summary>
    public int OutingTimes { get; set; } = 0;
}