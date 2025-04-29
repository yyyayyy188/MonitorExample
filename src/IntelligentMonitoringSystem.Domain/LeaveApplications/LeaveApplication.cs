using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using IntelligentMonitoringSystem.Domain.Shared.LeaveApplications.Enums;

namespace IntelligentMonitoringSystem.Domain.LeaveApplications;

/// <summary>
///     请假单
/// </summary>
public class LeaveApplication : AggregateRoot
{
    /// <summary>
    ///     面部Id
    /// </summary>
    public string FaceId { get; set; }

    /// <summary>
    ///     申请人Id
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    ///     请假单Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     请假单申请Id
    /// </summary>
    public string ApplicationId { get; set; }

    /// <summary>
    ///     请假单状态
    /// </summary>
    public LeaveApplicationStatusSmartEnum ApplicationStatus { get; set; }

    /// <summary>
    ///     开始时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    ///     结束时间
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    ///     审批时间
    /// </summary>
    public DateTime? ApprovalTime { get; set; }

    /// <summary>
    ///     外出原因
    /// </summary>
    public string Leave_reason { get; set; }

    /// <summary>
    ///     拒绝原因
    /// </summary>
    public string RejectionReason { get; set; }

    /// <summary>
    ///     申请人电话
    /// </summary>
    public string Telephone { get; set; }

    /// <summary>
    ///     申请人姓名
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    ///     请假时长
    /// </summary>
    public double DurationTime { get; set; }
}