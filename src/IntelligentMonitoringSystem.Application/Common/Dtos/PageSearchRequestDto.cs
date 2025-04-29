using IntelligentMonitoringSystem.Application.Shared.Dto.Pages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.Common.Dtos;

/// <summary>
///     分页查询请求
/// </summary>
public class PageSearchRequestDto : IPageSearchRequestDto
{
    /// <summary>
    ///     每页显示数量
    /// </summary>
    [JsonProperty("pageSize")]
    [FromQuery(Name = "pageSize")]
    public int PageSize { get; set; } = 10;

    /// <summary>
    ///     当前页码
    /// </summary>
    [JsonProperty("pageNo")]
    [FromQuery(Name = "pageNo")]
    public int PageNo { get; set; } = 1;
}