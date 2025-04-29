using IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Dtos;
using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.Shared.Dto.Pages;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;

namespace IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Search;

/// <summary>
///     搜索异常出入记录信息
/// </summary>
public class SearchAbnormalPersonnelAccessRecordQuery : QueryBase<PagedResultDto<SearchAbnormalPersonnelAccessRecordResponseDto>>
{
    /// <summary>
    ///     每页数量
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    ///     跳过数量
    /// </summary>
    public int SkipCount { get; set; }

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
}