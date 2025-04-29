namespace IntelligentMonitoringSystem.Application.Shared.Dto.Pages;

/// <summary>
///     分页查询请求
/// </summary>
public interface IPageSearchRequestDto
{
    /// <summary>
    ///     每页显示数量
    /// </summary>
    int PageSize { get; set; }

    /// <summary>
    ///     当前页码
    /// </summary>
    int PageNo { get; set; }
}