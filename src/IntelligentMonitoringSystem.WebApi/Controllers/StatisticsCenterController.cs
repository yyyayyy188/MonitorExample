using IntelligentMonitoringSystem.Application.Shared.Dto;
using IntelligentMonitoringSystem.Application.StatisticsCenters.ContrastStatistics;
using IntelligentMonitoringSystem.Application.StatisticsCenters.DeviceStatistics;
using IntelligentMonitoringSystem.Application.StatisticsCenters.Dto;
using IntelligentMonitoringSystem.Application.StatisticsCenters.StatisticsRanking;
using IntelligentMonitoringSystem.Application.StatisticsCenters.TendencyStatistics;
using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using IntelligentMonitoringSystem.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntelligentMonitoringSystem.WebApi.Controllers;

/// <summary>
///     TemplateController.
/// </summary>
/// <param name="sender">sender.</param>
[ApiController]
[CleanSvcRoute("/statistics-center")]
public class StatisticsCenterController(ISender sender) : ControllerBase
{
    /// <summary>
    ///     获取统计数据
    /// </summary>
    /// <returns>统计数据</returns>
    [HttpGet("tendency-statistics")]
    public async Task<ResponseEntity<TendencyStatisticsDto>> GetTendencyStatistics()
    {
        var result = await sender.Send(new TendencyStatisticsQuery());
        return ResponseEntity<TendencyStatisticsDto>.Ok(result);
    }

    /// <summary>
    ///     出入统计对比信息
    /// </summary>
    /// <returns>ContrastStatisticsDto</returns>
    [HttpGet("contrast-statistics")]
    public async Task<ResponseEntity<ContrastStatisticsDto>> GetContrastStatistics()
    {
        return ResponseEntity<ContrastStatisticsDto>.Ok(await sender.Send(new ContrastStatisticsQuery()));
    }

    /// <summary>
    ///     设备统计信息
    /// </summary>
    /// <returns>ContrastStatisticsDto</returns>
    [HttpGet("device-statistics")]
    public async Task<ResponseEntity<DeviceStatisticsDto>> GetDeviceStatistics()
    {
        return ResponseEntity<DeviceStatisticsDto>.Ok(await sender.Send(new DeviceStatisticsQuery()));
    }

    /// <summary>
    ///     排行榜
    /// </summary>
    /// <param name="type">排行版类型：count | average-time</param>
    /// <returns>排行列表</returns>
    [HttpGet("{type}/statistics-ranking")]
    public async Task<ResponseEntity<List<StatisticsRankingDto>>> StatisticsRanking([FromRoute] string type)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            throw new UserFriendlyException("参数错误，筛选类型不能空");
        }

        return ResponseEntity<List<StatisticsRankingDto>>.Ok(await sender.Send(new StatisticsRankingQuery(type.ToLower())));
    }
}