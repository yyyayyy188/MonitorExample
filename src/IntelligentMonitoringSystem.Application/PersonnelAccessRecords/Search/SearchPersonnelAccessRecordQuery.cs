using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Dtos;
using IntelligentMonitoringSystem.Application.Shared.Dto.Pages;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Search;

/// <summary>
///     查询人员出入记录
/// </summary>
public class SearchPersonnelAccessRecordQuery : QueryBase<PagedResultDto<SearchPersonnelAccessRecordResponseDto>>
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
    ///     出入状态
    /// </summary>
    public AccessStatusSmartEnum? AccessStatus { get; set; }

    /// <summary>
    ///     开始事件
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    ///     结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
}