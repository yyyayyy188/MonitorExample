using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Configs;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Const;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using MassTransit;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;

/// <summary>
///     异常出入记录
/// </summary>
public class AbnormalPersonnelAccessRecord : AggregateRoot
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AbnormalPersonnelAccessRecord" /> class.
    ///     构造函数
    /// </summary>
    public AbnormalPersonnelAccessRecord()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="AbnormalPersonnelAccessRecord" /> class.
    ///     构造函数
    /// </summary>
    /// <param name="personnelAccessRecordId">访问记录Id</param>
    /// <param name="leaveTime">离开时间</param>
    /// <param name="personnelAccessRecordConfig">配置信息</param>
    /// <param name="faceId">面容Id</param>
    public AbnormalPersonnelAccessRecord(int personnelAccessRecordId, DateTime leaveTime,
        PersonnelAccessRecordConfig personnelAccessRecordConfig, string faceId)
    {
        var currentDateTime = DateTime.Now;
        EventIdentifier = NewId.NextGuid();
        PersonnelAccessRecordId = personnelAccessRecordId;
        LeaveTime = leaveTime;
        CreateTime = currentDateTime;
        FaceId = faceId;
        NextWaringTime = Convert.ToDateTime(currentDateTime.ToString(PersonnelAccessRecordConst.FormatWaringDateTime))
            .AddSeconds(personnelAccessRecordConfig?.WaringIntervalSecond ?? 7200);
        Status = AbnormalPersonnelAccessRecordStatusSmartEnum.Out;
    }

    /// <summary>
    ///     Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     面容Id
    /// </summary>
    public string FaceId { get; set; }

    /// <summary>
    ///     关联出入记录Id
    /// </summary>
    public int PersonnelAccessRecordId { get; set; }

    /// <summary>
    ///     离开时间
    /// </summary>
    public DateTime LeaveTime { get; set; }

    /// <summary>
    ///     返回时间
    /// </summary>
    public DateTime? ReturnTime { get; set; }

    /// <summary>
    ///     下次告警时间
    /// </summary>
    public DateTime NextWaringTime { get; set; }

    /// <summary>
    ///     当前状态,I为在院内 O为在院外
    /// </summary>
    public AbnormalPersonnelAccessRecordStatusSmartEnum Status { get; set; }

    /// <summary>
    ///     创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    ///     最后操作时间
    /// </summary>
    public DateTime? LastEditTime { get; set; }

    /// <summary>
    ///     事件溯源唯一标识
    /// </summary>
    public Guid? EventIdentifier { get; set; }

    /// <summary>
    ///     关联访问记录表
    /// </summary>
    public PersonnelAccessRecord PersonnelAccessRecord { get; set; }
}