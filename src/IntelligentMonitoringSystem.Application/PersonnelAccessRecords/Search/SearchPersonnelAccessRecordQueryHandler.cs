using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Dtos;
using IntelligentMonitoringSystem.Application.Shared.Dto.Pages;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.OrderBys;
using IntelligentMonitoringSystem.Domain.Shared.Configs;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.Extensions;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using Microsoft.Extensions.Options;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Search;

/// <summary>
///     搜索人员出入记录
/// </summary>
public class SearchPersonnelAccessRecordQueryHandler(
    IOptionsMonitor<IntelligentMonitorSystemHttpClientConfig> options,
    IBasicRepository<PersonnelAccessRecord> personnelAccessRecordRepository)
    : IQueryHandler<SearchPersonnelAccessRecordQuery, PagedResultDto<SearchPersonnelAccessRecordResponseDto>>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>PagedResultDto<SearchPersonnelAccessRecordResponseDto />/></returns>
    public async Task<PagedResultDto<SearchPersonnelAccessRecordResponseDto>> Handle(
        SearchPersonnelAccessRecordQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<PersonnelAccessRecord>(true);
        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            predicate.And(x => x.Name.Contains(request.Name));
        }

        if (request.AccessStatus != null)
        {
            predicate.And(x => x.AccessStatus == request.AccessStatus);
        }

        if (request is { StartTime: not null, EndTime: not null })
        {
            predicate.And(x => x.AccessTime >= request.StartTime && x.AccessTime <= request.EndTime);
        }

        var totalCount = await personnelAccessRecordRepository.CountAsync(predicate);
        if (totalCount == 0)
        {
            return new PagedResultDto<SearchPersonnelAccessRecordResponseDto>(
                0,
                new List<SearchPersonnelAccessRecordResponseDto>());
        }

        var personnelAccessRecordList = await personnelAccessRecordRepository
            .PageAsync(request.PageSize, request.SkipCount, predicate,
                new PersonnelAccessRecordIdOrderBy(),
                x => new PersonnelAccessRecord
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    Gender = x.Gender,
                    AccessStatus = x.AccessStatus,
                    LeaveStatus = x.LeaveStatus,
                    OriginalFaceUrl = x.OriginalFaceUrl,
                    OriginalSnapUrl = x.OriginalSnapUrl,
                    AccessType = x.AccessType,
                    AccessTime = x.AccessTime,
                    FaceImgPath = x.FaceImgPath,
                    SnapImgPath = x.SnapImgPath,
                    LeaveApplicationId = x.LeaveApplicationId
                });
        return new PagedResultDto<SearchPersonnelAccessRecordResponseDto>
        {
            TotalCount = totalCount,
            Items = personnelAccessRecordList.Select(x => new SearchPersonnelAccessRecordResponseDto
            {
                PersonnelAccessRecordId = x.Id,
                Name = x.Name,
                Age = x.Age,
                Gender = x.Gender,
                AccessStatus = x.AccessType == AccessTypeSmartEnum.Enter ? AccessStatusSmartEnum.Normal : x.AccessStatus,
                AccessType = x.AccessType,
                AccessTime = x.AccessTime,
                FaceImgPath = x.GetFullFaceImgPath(options.CurrentValue) ?? x.OriginalFaceUrl,
                SnapImgPath = x.GetFullSnapImgPath(options.CurrentValue) ?? x.OriginalSnapUrl,
                LeaveApplicationId = x.LeaveApplicationId,
                LeaveStatus = x.LeaveStatus
            }).ToList()
        };
    }
}