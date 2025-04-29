namespace IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;

/// <summary>
///     领域事件记录
/// </summary>
public class DomainEventRecord
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DomainEventRecord" /> class.
    /// </summary>
    /// <param name="eventData">eventData.</param>
    public DomainEventRecord(IDomainEvent eventData)
    {
        EventData = eventData;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DomainEventRecord" /> class.
    /// </summary>
    /// <param name="eventData">eventData.</param>
    /// <param name="eventOrder">eventOrder.</param>
    public DomainEventRecord(IDomainEvent eventData, int eventOrder)
    {
        EventData = eventData;
        EventOrder = eventOrder;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DomainEventRecord" /> class.
    /// </summary>
    /// <param name="eventData">eventData.</param>
    /// <param name="eventOrder">eventOrder.</param>
    /// <param name="isImmediateProcessing">isImmediateProcessing</param>
    public DomainEventRecord(IDomainEvent eventData, int eventOrder, bool isImmediateProcessing)
    {
        EventData = eventData;
        EventOrder = eventOrder;
        IsImmediateProcessing = isImmediateProcessing;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DomainEventRecord" /> class.
    /// </summary>
    /// <param name="eventData">eventData.</param>
    /// <param name="isImmediateProcessing">isImmediateProcessing</param>
    public DomainEventRecord(IDomainEvent eventData, bool isImmediateProcessing)
    {
        EventData = eventData;
        IsImmediateProcessing = isImmediateProcessing;
    }

    /// <summary>
    ///     事件数据
    /// </summary>
    public IDomainEvent EventData { get; }

    /// <summary>
    ///     事件排序 值越大越优先消费
    /// </summary>
    public int? EventOrder { get; }

    /// <summary>
    ///     是否立即处理
    /// </summary>
    public bool? IsImmediateProcessing { get; }
}