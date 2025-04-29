using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Dtos;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Export;

/// <summary>
///     导出出入记录
/// </summary>
public class ExportPersonnelAccessQuery : QueryBase<IEnumerable<ExportPersonnelAccessRecordDto>>
{
    /// <summary>
    ///     姓名
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     出入状态 Abnormal:异常 Normal:正常 Pending:暂无
    /// </summary>
    public AccessStatusSmartEnum? AccessStatus { get; set; }

    /// <summary>
    ///     开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    ///     结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    ///     出入记录编号
    /// </summary>
    public List<int>? PersonnelAccessRecordIds { get; set; }
}