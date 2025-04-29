#nullable enable
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Dtos;
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Export;
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Get;
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.GetVideoPlayback;
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Search;
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.UpdateEnter;
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.UpdateLeave;
using IntelligentMonitoringSystem.Application.Shared.Dto;
using IntelligentMonitoringSystem.Application.Shared.Dto.Const;
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
[CleanSvcRoute("/personnel-access-record")]
public class PersonnelAccessRecordController(ISender sender) : ControllerBase
{
    /// <summary>
    ///     视频回放
    /// </summary>
    /// <param name="personnelAccessRecordId">访问记录Id</param>
    /// <returns>视频地址</returns>
    [HttpGet("{personnelAccessRecordId:int}/video-playback")]
    public async Task<ResponseEntity<string?>> GetVideoPlayback([FromRoute] int personnelAccessRecordId)
    {
        return ResponseEntity<string?>.Ok(
            await sender.Send(new GetVideoPlaybackQuery { PersonnelAccessRecordId = personnelAccessRecordId }));
    }

    /// <summary>
    ///     更新离开状态
    /// </summary>
    /// <param name="request">请求对象</param>
    /// <param name="personnelAccessRecordId">出入</param>
    /// <param name="userId">用户Id</param>
    /// <returns>ResponseEntity{bool}</returns>
    [HttpPut("{personnelAccessRecordId}/leave/access-status/")]
    [ServiceFilter(typeof(ValidationFilter<UpdateLeaveAccessStatusDto>))]
    public async Task<ResponseEntity<bool>> UpdateLeaveAccessStatus(
        [FromBody] UpdateLeaveAccessStatusDto request,
        [FromRoute] int personnelAccessRecordId,
        [FromHeader(Name = AuthHeaderConst.AuthUserHeaderName)]
        string userId)
    {
        if (personnelAccessRecordId <= 0)
        {
            throw new UserFriendlyException($"the request param {personnelAccessRecordId} is not allowed.");
        }

        await sender.Send(new UpdateLeaveAccessStatusCommand
        {
            PersonnelAccessRecordId = personnelAccessRecordId,
            AccessStatus = request.AccessStatus,
            Remark = request.Remark,
            UserId = userId
        });

        return ResponseEntity<bool>.Ok(true);
    }

    /// <summary>
    ///     更新进入状态
    /// </summary>
    /// <param name="request">请求对象</param>
    /// <param name="personnelAccessRecordId">出入</param>
    /// <param name="userId">用户Id</param>
    /// <returns>ResponseEntity{bool}</returns>
    [HttpPut("{personnelAccessRecordId}/enter/access-status/")]
    [ServiceFilter(typeof(ValidationFilter<UpdateEnterAccessStatusDto>))]
    public async Task<ResponseEntity<bool>> UpdateEnterAccessStatus(
        [FromBody] UpdateEnterAccessStatusDto request,
        [FromRoute] int personnelAccessRecordId,
        [FromHeader(Name = AuthHeaderConst.AuthUserHeaderName)]
        string userId)
    {
        if (personnelAccessRecordId <= 0)
        {
            throw new UserFriendlyException($"the request param {personnelAccessRecordId} is not allowed.");
        }

        await sender.Send(new UpdateEnterAccessStatusCommand
        {
            PersonnelAccessRecordId = personnelAccessRecordId, Remark = request.Remark, UserId = userId
        });

        return ResponseEntity<bool>.Ok(true);
    }

    /// <summary>
    ///     查询出入记录
    /// </summary>
    /// <param name="request">request</param>
    /// <returns>ResponseEntity<object /></returns>
    [HttpGet("search")]
    [ServiceFilter(typeof(ValidationFilter<SearchPersonnelAccessRequestDto>))]
    public async Task<ResponseEntity<PagedResultDto<SearchPersonnelAccessRecordResponseDto>>>
        SearchPersonnelAccessRecord([FromQuery] SearchPersonnelAccessRequestDto request)
    {
        var searchPersonnelAccessRecordQuery =
            new SearchPersonnelAccessRecordQuery
            {
                AccessStatus = request.AccessStatus != null ? AccessStatusSmartEnum.FromName(request.AccessStatus) : null,
                EndTime = request.EndTime,
                Name = string.IsNullOrWhiteSpace(request.Name) ? null : request.Name.Trim(),
                SkipCount = (request.PageNo - 1) * request.PageSize,
                PageSize = request.PageSize,
                StartTime = request.StartTime
            };
        var result = await sender.Send(searchPersonnelAccessRecordQuery);

        return ResponseEntity<PagedResultDto<SearchPersonnelAccessRecordResponseDto>>.Ok(result);
    }

    /// <summary>
    ///     查询出入记录详情
    /// </summary>
    /// <param name="personnelAccessRecordId">出入记录单Id</param>
    /// <returns>ResponseEntity<PersonnelAccessRecordDetailDto /></returns>
    [HttpGet("{personnelAccessRecordId:int}")]
    public async Task<ResponseEntity<PersonnelAccessRecordDetailDto>> GetPersonnelAccessRecord(
        [FromRoute] int personnelAccessRecordId)
    {
        if (personnelAccessRecordId <= 0)
        {
            throw new UserFriendlyException($"不存在编号为：{personnelAccessRecordId} 的记录");
        }

        var result = await sender.Send(new GetPersonnelAccessRecordQuery { PersonnelAccessRecordId = personnelAccessRecordId });
        return ResponseEntity<PersonnelAccessRecordDetailDto>.Ok(result);
    }

    /// <summary>
    ///     导出出入记录
    /// </summary>
    /// <param name="requestDto">刷选条件</param>
    /// <returns>FileStreamHttpResult</returns>
    [HttpPost("export")]
    public async Task<FileStreamHttpResult> ExportPersonnelAccessRecord([FromBody] ExportPersonnelAccessRequestDto requestDto)
    {
        var result = await sender.Send(new ExportPersonnelAccessQuery
        {
            Name = string.IsNullOrWhiteSpace(requestDto.Name) ? null : requestDto.Name.Trim(),
            AccessStatus = requestDto.AccessStatus != null ? AccessStatusSmartEnum.FromName(requestDto.AccessStatus) : null,
            EndTime = requestDto.EndTime,
            StartTime = requestDto.StartTime,
            PersonnelAccessRecordIds = requestDto.PersonnelAccessRecordIds
        });
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(result);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return TypedResults.File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"出入历史记录{DateTime.Now:yyyyMMdd}.xlsx");
    }
}