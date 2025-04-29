using Ardalis.SmartEnum;

namespace IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;

/// <summary>
///     访问状态 Abnormal:异常 Normal:正常 Pending:暂无
/// </summary>
[SmartEnumStringComparer(StringComparison.InvariantCultureIgnoreCase)]
public class AccessStatusSmartEnum : SmartEnum<AccessStatusSmartEnum, string>
{
    /// <summary>
    ///     暂无
    /// </summary>
    public readonly static AccessStatusSmartEnum Pending = new PendingAccessStatusSmartEnum();

    /// <summary>
    ///     正常
    /// </summary>
    public readonly static AccessStatusSmartEnum Normal = new NormalAccessStatusSmartEnum();

    /// <summary>
    ///     异常
    /// </summary>
    public readonly static AccessStatusSmartEnum Abnormal = new AbnormalAccessStatusSmartEnum();

    /// <summary>
    ///     Initializes a new instance of the <see cref="AccessStatusSmartEnum" /> class.
    ///     构造函数
    /// </summary>
    /// <param name="name">name</param>
    /// <param name="value">value</param>
    private AccessStatusSmartEnum(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    ///     描述信息
    /// </summary>
    public virtual string DisplayName { get; set; }

    /// <summary>
    ///     暂无
    /// </summary>
    private sealed class PendingAccessStatusSmartEnum() : AccessStatusSmartEnum("Pending", "P")
    {
        public override string DisplayName => "暂无";
    }

    /// <summary>
    ///     正常
    /// </summary>
    private sealed class NormalAccessStatusSmartEnum() : AccessStatusSmartEnum("Normal", "N")
    {
        public override string DisplayName => "正常";
    }

    /// <summary>
    ///     异常
    /// </summary>
    private sealed class AbnormalAccessStatusSmartEnum() : AccessStatusSmartEnum("Abnormal", "A")
    {
        public override string DisplayName => "异常";
    }
}