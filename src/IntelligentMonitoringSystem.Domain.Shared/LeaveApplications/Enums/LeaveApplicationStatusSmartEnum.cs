using Ardalis.SmartEnum;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;

namespace IntelligentMonitoringSystem.Domain.Shared.LeaveApplications.Enums;

/// <summary>
///     请假申请状态枚举
/// </summary>
public class LeaveApplicationStatusSmartEnum : SmartEnum<LeaveApplicationStatusSmartEnum>
{
    /// <summary>
    ///     待审批
    /// </summary>
    public readonly static LeaveApplicationStatusSmartEnum Pending = new PendingSmartEnum();

    /// <summary>
    ///     通过
    /// </summary>
    public readonly static LeaveApplicationStatusSmartEnum Pass = new PassSmartEnum();

    /// <summary>
    ///     驳回
    /// </summary>
    public readonly static LeaveApplicationStatusSmartEnum Reject = new RejectSmartEnum();

    /// <summary>
    ///     超时驳回
    /// </summary>
    public readonly static LeaveApplicationStatusSmartEnum TimeOutReject = new TimeOutRejectSmartEnum();

    /// <summary>
    ///     Initializes a new instance of the <see cref="LeaveApplicationStatusSmartEnum" /> class.
    ///     构造函数
    /// </summary>
    /// <param name="name">name</param>
    /// <param name="value">value</param>
    private LeaveApplicationStatusSmartEnum(string name, int value) : base(name, value)
    {
    }

    /// <summary>
    ///     请假状态
    /// </summary>
    public virtual LeaveStatusSmartEnum LeaveStatus { get; set; }

    /// <summary>
    ///     待审批
    /// </summary>
    private sealed class PendingSmartEnum() : LeaveApplicationStatusSmartEnum("Pending", 0)
    {
        public override LeaveStatusSmartEnum LeaveStatus => LeaveStatusSmartEnum.Pending;
    }

    /// <summary>
    ///     通过
    /// </summary>
    private sealed class PassSmartEnum() : LeaveApplicationStatusSmartEnum("Pass", 1)
    {
        public override LeaveStatusSmartEnum LeaveStatus => LeaveStatusSmartEnum.Success;
    }

    /// <summary>
    ///     驳回
    /// </summary>
    private sealed class RejectSmartEnum() : LeaveApplicationStatusSmartEnum("Reject", 2)
    {
        public override LeaveStatusSmartEnum LeaveStatus => LeaveStatusSmartEnum.Failed;
    }

    /// <summary>
    ///     超时驳回
    /// </summary>
    private sealed class TimeOutRejectSmartEnum() : LeaveApplicationStatusSmartEnum("TimeOutReject", 3)
    {
        public override LeaveStatusSmartEnum LeaveStatus => LeaveStatusSmartEnum.Failed;
    }
}