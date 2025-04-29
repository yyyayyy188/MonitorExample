using System.Globalization;
using IntelligentMonitoringSystem.Application.MonitoringCenters.AbnormalPersonnelAccessRecordStatistics;
using IntelligentMonitoringSystem.Application.MonitoringCenters.Dtos;
using IntelligentMonitoringSystem.Application.MonitoringCenters.NewestRecord;
using IntelligentMonitoringSystem.Application.MonitoringCenters.PersonnelAccessRecordStatistics;
using IntelligentMonitoringSystem.Application.MonitoringCenters.QueryMonthAbnormalPersonnelAccessRecordStatistics;
using IntelligentMonitoringSystem.Application.Shared.Dto;
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
[CleanSvcRoute("/monitoring-center")]
public class MonitoringCenterController(ISender sender) : ControllerBase
{
    /// <summary>
    ///     获取最新的记录
    /// </summary>
    /// <param name="size">数量</param>
    /// <returns>List<NewestPersonnelAccessRecordDto /></returns>
    [HttpGet("newest-record")]
    public async Task<List<NewestPersonnelAccessRecordDto>> GetNewestRecord([FromQuery] int size = 10)
    {
        if (size <= 0)
        {
            return [];
        }

        return await sender.Send(new NewestRecordQuery { Size = size });
    }

    /// <summary>
    ///     出入查询统计信息
    /// </summary>
    /// <param name="filterDateTime">筛选时间，格式规则为 20241206</param>
    /// <returns>PersonnelAccessRecordStatisticsDto</returns>
    [HttpGet("personnel-access-record/{filterDateTime}/statistics")]
    public async Task<ResponseEntity<PersonnelAccessRecordStatisticsDto>> GetPersonnelAccessRecordStatistics(
        [FromRoute] string filterDateTime)
    {
        if (string.IsNullOrWhiteSpace(filterDateTime))
        {
            throw new UserFriendlyException("筛选时间格式错误");
        }

        if (!DateTime.TryParseExact(filterDateTime, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var result))
        {
            throw new UserFriendlyException("筛选时间格式错误");
        }

        return ResponseEntity<PersonnelAccessRecordStatisticsDto>.Ok(
            await sender.Send(new PersonnelAccessRecordStatisticsQuery(result)));
    }

    /// <summary>
    ///     月度异常出入查询统计信息
    /// </summary>
    /// <param name="filterMonthTime">筛选时间，格式规则为 202412</param>
    /// <returns>PersonnelAccessRecordStatisticsDto</returns>
    [HttpGet("abnormal-personnel-access-record/{filterMonthTime}/statistics")]
    public async Task<ResponseEntity<IList<int>>> QueryMonthAbnormalPersonnelAccessRecordStatistics(
        [FromRoute] string filterMonthTime)
    {
        if (string.IsNullOrWhiteSpace(filterMonthTime))
        {
            throw new UserFriendlyException("筛选时间格式错误");
        }

        if (!DateTime.TryParseExact(filterMonthTime, "yyyyMM", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var result))
        {
            throw new UserFriendlyException("筛选时间格式错误");
        }

        return ResponseEntity<IList<int>>.Ok(
            await sender.Send(new QueryMonthAbnormalPersonnelAccessRecordStatisticsQuery(result)));
    }

    /// <summary>
    ///     异常出入查询统计信息
    /// </summary>
    /// <returns>PersonnelAccessRecordStatisticsDto</returns>
    [HttpGet("abnormal-personnel-access-record/statistics")]
    public async Task<ResponseEntity<AbnormalPersonnelAccessRecordStatisticsDto>> GetAbnormalPersonnelAccessRecordStatistics()
    {
        return ResponseEntity<AbnormalPersonnelAccessRecordStatisticsDto>.Ok(
            await sender.Send(new AbnormalPersonnelAccessRecordStatisticsQuery()));
    }
}