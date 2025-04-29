using Ardalis.SmartEnum;

namespace IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;

/// <summary>
///     异常出入记录状态
/// </summary>
[SmartEnumStringComparer(StringComparison.InvariantCultureIgnoreCase)]
public class AbnormalPersonnelAccessRecordStatusSmartEnum : SmartEnum<AbnormalPersonnelAccessRecordStatusSmartEnum, string>
{
    /// <summary>
    ///     院内
    /// </summary>
    public readonly static AbnormalPersonnelAccessRecordStatusSmartEnum In = new InSmartEnum();

    /// <summary>
    ///     院外
    /// </summary>
    public readonly static AbnormalPersonnelAccessRecordStatusSmartEnum Out = new OutSmartEnum();

    /// <summary>
    ///     Initializes a new instance of the <see cref="AbnormalPersonnelAccessRecordStatusSmartEnum" /> class.
    ///     构造函数
    /// </summary>
    /// <param name="name">name</param>
    /// <param name="value">value</param>
    private AbnormalPersonnelAccessRecordStatusSmartEnum(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    ///     显示名称
    /// </summary>
    public virtual string DisplayName { get; set; }

    /// <summary>
    ///     院内
    /// </summary>
    private sealed class InSmartEnum() : AbnormalPersonnelAccessRecordStatusSmartEnum("In", "I")
    {
        public override string DisplayName => "在院内";
    }

    /// <summary>
    ///     院外
    /// </summary>
    private sealed class OutSmartEnum() : AbnormalPersonnelAccessRecordStatusSmartEnum("Out", "O")
    {
        public override string DisplayName => "在院外";
    }
}