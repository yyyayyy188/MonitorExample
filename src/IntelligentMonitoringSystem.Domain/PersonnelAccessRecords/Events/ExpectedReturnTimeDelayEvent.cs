using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Events;

/// <summary>
///     预计返回时间延迟事件
/// </summary>
public class ExpectedReturnTimeDelayEvent : DomainEventBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ExpectedReturnTimeDelayEvent" /> class.
    /// </summary>
    public ExpectedReturnTimeDelayEvent()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ExpectedReturnTimeDelayEvent" /> class.
    ///     构造函数
    /// </summary>
    /// <param name="eventIdentifier">eventIdentifier</param>
    /// <param name="expectedReturnTime">expectedReturnTime</param>
    public ExpectedReturnTimeDelayEvent(Guid eventIdentifier, DateTime expectedReturnTime)
    {
        EventIdentifier = eventIdentifier;
        ExpectedReturnTime = expectedReturnTime;
    }

    /// <summary>
    ///     获取事件流Id
    /// </summary>
    public override string StreamId => EventIdentifier.ToString();

    /// <summary>
    ///     人员出入记录事件Id
    /// </summary>
    public Guid EventIdentifier { get; set; }

    /// <summary>
    ///     预计返回时间
    /// </summary>
    public DateTime ExpectedReturnTime { get; set; }
}