using Ardalis.SmartEnum;

namespace IntelligentMonitoringSystem.Domain.Shared.EventSources.Enums;

/// <summary>
///     事件来源类型
/// </summary>
[SmartEnumStringComparer(StringComparison.InvariantCultureIgnoreCase)]
public class EventSourceTypeSmartEnum : SmartEnum<EventSourceTypeSmartEnum>
{
    /// <summary>
    ///     院内
    /// </summary>
    public readonly static EventSourceTypeSmartEnum In = new SynchronizationImg();

    /// <summary>
    ///     Initializes a new instance of the <see cref="EventSourceTypeSmartEnum" /> class.
    ///     构造函数
    /// </summary>
    /// <param name="name">name</param>
    /// <param name="value">value</param>
    private EventSourceTypeSmartEnum(string name, int value) : base(name, value)
    {
    }

    /// <summary>
    ///     显示名称
    /// </summary>
    public virtual string DisplayName { get; set; }

    /// <summary>
    ///     同步图片
    /// </summary>
    private sealed class SynchronizationImg() : EventSourceTypeSmartEnum("SynchronizationImg", 0)
    {
        public override string DisplayName => "同步图片";
    }
}