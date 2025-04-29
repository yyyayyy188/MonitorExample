using IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Dtos;
using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.Shared.Dto.Pages;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;

namespace IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Search;

/// <summary>
///     搜索异常出入记录
/// </summary>
public class SearchAbnormalPersonnelAccessRecordQueryHandler(
    IAbnormalPersonnelAccessRecordRepository abnormalPersonnelAccessRecordRepository)
    : IQueryHandler<SearchAbnormalPersonnelAccessRecordQuery,
        PagedResultDto<SearchAbnormalPersonnelAccessRecordResponseDto>>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>PagedResultDto<SearchAbnormalPersonnelAccessRecordResponseDto /></returns>
    public async Task<PagedResultDto<SearchAbnormalPersonnelAccessRecordResponseDto>> Handle(
        SearchAbnormalPersonnelAccessRecordQuery request,
        CancellationToken cancellationToken)
    {
        var totalCount = await abnormalPersonnelAccessRecordRepository.SearchTotalCountAsync(request.Name, request.Status,
            request.LeaveStartTime, request.LeaveEndTime,
            request.EnterStartTime, request.EnterEndTime);
        if (totalCount <= 0)
        {
            return new PagedResultDto<SearchAbnormalPersonnelAccessRecordResponseDto>(
                0,
                new List<SearchAbnormalPersonnelAccessRecordResponseDto>());
        }

        var abnormalPersonnelAccessRecordList = await abnormalPersonnelAccessRecordRepository.PageSearchAsync(
            request.PageSize, request.SkipCount, request.Name, request.Status, request.LeaveStartTime, request.LeaveEndTime,
            request.EnterStartTime, request.EnterEndTime);
        return new PagedResultDto<SearchAbnormalPersonnelAccessRecordResponseDto>(
            totalCount,
            abnormalPersonnelAccessRecordList.ConvertAll(x => new SearchAbnormalPersonnelAccessRecordResponseDto
            {
                Id = x.Id,
                Name = x.PersonnelAccessRecord.Name,
                Age = x.PersonnelAccessRecord.Age,
                Gender = x.PersonnelAccessRecord.Gender,
                Status = x.Status,
                LeaveTime = x.LeaveTime,
                ReturnTime = x.ReturnTime,
                Nurse = x.PersonnelAccessRecord.Nurse,
                RoomNo = x.PersonnelAccessRecord.RoomNo,
                LeaveStatus = x.PersonnelAccessRecord.LeaveStatus,
                LeaveApplicationId = x.PersonnelAccessRecord.LeaveApplicationId,
                TotalTimes = (long)((x.ReturnTime ?? DateTime.Now) - x.LeaveTime).TotalSeconds
            }));
    }
}