using IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Dtos;
using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;

namespace IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Export;

/// <summary>
///     导出异常人员出入记录
/// </summary>
public class ExportAbnormalPersonnelAccessQuery : QueryBase<IEnumerable<ExportAbnormalPersonnelAccessDto>>
{
    /// <summary>
    ///     姓名
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     状态 In 在院内,Out 在院外
    /// </summary>
    public AbnormalPersonnelAccessRecordStatusSmartEnum? Status { get; set; }

    /// <summary>
    ///     离开开始时间
    /// </summary>
    public DateTime? LeaveStartTime { get; set; }

    /// <summary>
    ///     离开结束时间
    /// </summary>
    public DateTime? LeaveEndTime { get; set; }

    /// <summary>
    ///     返回开始时间
    /// </summary>
    public DateTime? EnterStartTime { get; set; }

    /// <summary>
    ///     返回结束时间
    /// </summary>
    public DateTime? EnterEndTime { get; set; }

    /// <summary>
    ///     ids
    /// </summary>
    public List<int>? Ids { get; set; }
}