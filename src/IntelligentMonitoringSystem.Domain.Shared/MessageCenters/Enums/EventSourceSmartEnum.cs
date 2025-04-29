using Ardalis.SmartEnum;
using IntelligentMonitoringSystem.Domain.Shared.MessageCenters.Const;

namespace IntelligentMonitoringSystem.Domain.Shared.MessageCenters.Enums;

/// <summary>
///     事件来源
/// </summary>
/// <param name="name">名称</param>
/// <param name="value">值</param>
public class EventSourceSmartEnum(string name, string value) : SmartEnum<EventSourceSmartEnum, string>(name, value)
{
    /// <summary>
    ///     进入
    /// </summary>
    public readonly static EventSourceSmartEnum Enter = new EnterEventSmartEnum();

    /// <summary>
    ///     异常离开
    /// </summary>
    public readonly static EventSourceSmartEnum AbnormalLeave = new AbnormalLeaveEventSmartEnum();

    /// <summary>
    ///     离开
    /// </summary>
    public readonly static EventSourceSmartEnum Leave = new LeaveEventSmartEnum();

    /// <summary>
    ///     异常外出返回
    /// </summary>
    public readonly static EventSourceSmartEnum AbnormalEnter = new AbnormalEnterEventSmartEnum();

    /// <summary>
    ///     异常告警
    /// </summary>
    public readonly static EventSourceSmartEnum AbnormalWaring = new AbnormalWaringSmartEnum();

    /// <summary>
    ///     消息数据解析提供者
    /// </summary>
    public virtual string MessageDataResolverProvider { get; set; }

    /// <summary>
    ///     进入事件
    /// </summary>
    private sealed class EnterEventSmartEnum() : EventSourceSmartEnum("Enter", "E")
    {
        public override string MessageDataResolverProvider =>
            MessageDataResolverConst.AccessRecordMessageDataResolverProvider;
    }

    /// <summary>
    ///     离开事件
    /// </summary>
    private sealed class LeaveEventSmartEnum() : EventSourceSmartEnum("Leave", "L")
    {
        public override string MessageDataResolverProvider =>
            MessageDataResolverConst.AccessRecordMessageDataResolverProvider;
    }

    /// <summary>
    ///     异常离开事件
    /// </summary>
    private sealed class AbnormalLeaveEventSmartEnum() : EventSourceSmartEnum("AbnormalLeave", "AL")
    {
        public override string MessageDataResolverProvider =>
            MessageDataResolverConst.AbnormalWaringMessageDataResolverProvider;
    }

    /// <summary>
    ///     异常外出返回消息
    /// </summary>
    private sealed class AbnormalEnterEventSmartEnum() : EventSourceSmartEnum("AbnormalEnter", "AE")
    {
        public override string MessageDataResolverProvider =>
            MessageDataResolverConst.AccessRecordMessageDataResolverProvider;
    }

    /// <summary>
    ///     异常告警通知
    /// </summary>
    private sealed class AbnormalWaringSmartEnum() : EventSourceSmartEnum("AbnormalEnter", "AW")
    {
        public override string MessageDataResolverProvider =>
            MessageDataResolverConst.AccessRecordMessageDataResolverProvider;
    }
}