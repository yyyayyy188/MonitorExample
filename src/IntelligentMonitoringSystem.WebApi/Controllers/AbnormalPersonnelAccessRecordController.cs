using IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Dtos;
using IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Export;
using IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Get;
using IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Search;
using IntelligentMonitoringSystem.Application.Shared.Dto;
using IntelligentMonitoringSystem.Application.Shared.Dto.Pages;
using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using IntelligentMonitoringSystem.WebApi.Extensions;
using IntelligentMonitoringSystem.WebApi.Validators;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MiniExcelLibs;

namespace IntelligentMonitoringSystem.WebApi.Controllers;

/// <summary>
///     TemplateController.
/// </summary>
/// <param name="sender">sender.</param>
[ApiController]
[CleanSvcRoute("/abnormal-personnel-access-record")]
public class AbnormalPersonnelAccessRecordController(ISender sender) : ControllerBase
{
    /// <summary>
    ///     查询异常出入记录
    /// </summary>
    /// <param name="request">request</param>
    /// <returns>ResponseEntity<object /></returns>
    [HttpGet("search")]
    [ServiceFilter(typeof(ValidationFilter<SearchAbnormalPersonnelAccessRequestDto>))]
    public async Task<ResponseEntity<PagedResultDto<SearchAbnormalPersonnelAccessRecordResponseDto>>>
        SearchAbnormalPersonnelAccessRecord([FromQuery] SearchAbnormalPersonnelAccessRequestDto request)
    {
        var result = await sender.Send(new SearchAbnormalPersonnelAccessRecordQuery
        {
            PageSize = request.PageSize,
            SkipCount = (request.PageNo - 1) * request.PageSize,
            Name = string.IsNullOrWhiteSpace(request.Name) ? null : request.Name.Trim(),
            Status =
                string.IsNullOrWhiteSpace(request.Status)
                    ? null
                    : AbnormalPersonnelAccessRecordStatusSmartEnum.FromName(request.Status),
            LeaveStartTime = request.LeaveStartTime,
            LeaveEndTime = request.LeaveEndTime,
            EnterStartTime = request.EnterStartTime,
            EnterEndTime = request.EnterEndTime
        });

        return ResponseEntity<PagedResultDto<SearchAbnormalPersonnelAccessRecordResponseDto>>.Ok(result);
    }

    /// <summary>
    ///     查询异常出入记录详情
    /// </summary>
    /// <param name="id">异常记录单Id</param>
    /// <returns>ResponseEntity<object /></returns>
    [HttpGet("{id:int}")]
    public async Task<ResponseEntity<AbnormalPersonnelAccessRecordDetailDto>> GetAbnormalPersonnelAccessRecord(
        [FromRoute] int id)
    {
        if (id <= 0)
        {
            throw new UserFriendlyException($"不存在编号为：{id} 的记录");
        }

        var result = await sender.Send(new GetAbnormalPersonnelAccessRecordQuery(id));

        return ResponseEntity<AbnormalPersonnelAccessRecordDetailDto>.Ok(result);
    }

    /// <summary>
    ///     导出异常出入记录
    /// </summary>
    /// <param name="requestDto">刷选条件</param>
    /// <returns>FileStreamHttpResult</returns>
    [HttpPost("export")]
    public async Task<FileStreamHttpResult> ExportPersonnelAccessRecord(
        [FromBody] ExportAbnormalPersonnelAccessRequestDto requestDto)
    {
        var result = await sender.Send(new ExportAbnormalPersonnelAccessQuery
        {
            Name = string.IsNullOrWhiteSpace(requestDto.Name) ? null : requestDto.Name.Trim(),
            Status =
                requestDto.Status != null ? AbnormalPersonnelAccessRecordStatusSmartEnum.FromName(requestDto.Status) : null,
            LeaveStartTime = requestDto.LeaveStartTime,
            LeaveEndTime = requestDto.LeaveEndTime,
            EnterStartTime = requestDto.EnterStartTime,
            EnterEndTime = requestDto.EnterEndTime,
            Ids = requestDto.Ids
        });
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(result);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return TypedResults.File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"异常出入记录{DateTime.Now:yyyyMMdd}.xlsx");
    }
}