using Ardalis.SmartEnum;

namespace IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;

/// <summary>
///     请假状态： 请假未通过P;未请假W;请假成功S;请假被拒绝F
/// </summary>
[SmartEnumStringComparer(StringComparison.InvariantCultureIgnoreCase)]
public class LeaveStatusSmartEnum : SmartEnum<LeaveStatusSmartEnum, string>
{
    /// <summary>
    ///     请假未通过
    /// </summary>
    public readonly static LeaveStatusSmartEnum Pending = new PendingLeaveStatusSmartEnum();

    /// <summary>
    ///     未请假
    /// </summary>
    public readonly static LeaveStatusSmartEnum WithoutLeave = new WithoutLeaveStatusSmartEnum();

    /// <summary>
    ///     请假成功
    /// </summary>
    public readonly static LeaveStatusSmartEnum Success = new SuccessLeaveStatusSmartEnum();

    /// <summary>
    ///     请假被拒绝
    /// </summary>
    public readonly static LeaveStatusSmartEnum Failed = new FailedLeaveStatusSmartEnum();

    /// <summary>
    ///     Initializes a new instance of the <see cref="LeaveStatusSmartEnum" /> class.
    ///     构造函数
    /// </summary>
    /// <param name="name">name</param>
    /// <param name="value">value</param>
    private LeaveStatusSmartEnum(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    ///     备注名称
    /// </summary>
    public virtual string DisplayName { get; set; }

    /// <summary>
    ///     待审批
    /// </summary>
    private sealed class PendingLeaveStatusSmartEnum() : LeaveStatusSmartEnum("Pending", "P")
    {
        public override string DisplayName => "待审批";
    }

    /// <summary>
    ///     未请假
    /// </summary>
    private sealed class WithoutLeaveStatusSmartEnum() : LeaveStatusSmartEnum("WithoutLeave", "W")
    {
        public override string DisplayName => "--";
    }

    /// <summary>
    ///     请假成功
    /// </summary>
    private sealed class SuccessLeaveStatusSmartEnum() : LeaveStatusSmartEnum("Success", "S")
    {
        public override string DisplayName => "已通过";
    }

    /// <summary>
    ///     请假未通过
    /// </summary>
    private sealed class FailedLeaveStatusSmartEnum() : LeaveStatusSmartEnum("Failed", "F")
    {
        public override string DisplayName => "已驳回";
    }
}