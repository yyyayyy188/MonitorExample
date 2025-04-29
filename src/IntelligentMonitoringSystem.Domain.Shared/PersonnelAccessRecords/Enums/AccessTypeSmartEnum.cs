using Ardalis.SmartEnum;

namespace IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;

/// <summary>
///     访问类型
/// </summary>
[SmartEnumStringComparer(StringComparison.InvariantCultureIgnoreCase)]
public class AccessTypeSmartEnum : SmartEnum<AccessTypeSmartEnum, string>
{
    /// <summary>
    ///     进入
    /// </summary>
    public readonly static AccessTypeSmartEnum Enter = new EnterAccessTypeSmartEnum();

    /// <summary>
    ///     离开
    /// </summary>
    public readonly static AccessTypeSmartEnum Leave = new LeaveAccessTypeSmartEnum();

    /// <summary>
    ///     Initializes a new instance of the <see cref="AccessTypeSmartEnum" /> class.
    ///     构造函数
    /// </summary>
    /// <param name="name">name</param>
    /// <param name="value">value</param>
    private AccessTypeSmartEnum(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    ///     描述信息
    /// </summary>
    public virtual string DisplayName { get; set; }

    /// <summary>
    ///     进入
    /// </summary>
    private sealed class EnterAccessTypeSmartEnum() : AccessTypeSmartEnum("Enter", "E")
    {
        public override string DisplayName => "进入";
    }

    /// <summary>
    ///     离开
    /// </summary>
    private sealed class LeaveAccessTypeSmartEnum() : AccessTypeSmartEnum("Leave", "L")
    {
        public override string DisplayName => "外出";
    }
}