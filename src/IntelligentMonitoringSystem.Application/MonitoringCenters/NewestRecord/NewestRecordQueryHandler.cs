using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.MonitoringCenters.Dtos;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.OrderBys;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;

namespace IntelligentMonitoringSystem.Application.MonitoringCenters.NewestRecord;

/// <summary>
///     最新出入记录信息
/// </summary>
public class NewestRecordQueryHandler(IBasicRepository<PersonnelAccessRecord> basicRepository)
    : IQueryHandler<NewestRecordQuery, List<NewestPersonnelAccessRecordDto>>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>PersonnelAccessRecordDetailDto</returns>
    public async Task<List<NewestPersonnelAccessRecordDto>> Handle(NewestRecordQuery request, CancellationToken cancellationToken)
    {
        var currentDateTime = DateTime.Now;
        var beginTime = currentDateTime.Date;
        var endTime = beginTime.AddDays(1).AddSeconds(-1);
        var records = await basicRepository.PageAsync(request.Size, 0,
            x => x.AccessTime >= beginTime && x.AccessTime <= endTime,
            new PersonnelAccessRecordIdOrderBy(),
            x => new PersonnelAccessRecord
            {
                Id = x.Id,
                Name = x.Name,
                Age = x.Age,
                Gender = x.Gender,
                AccessTime = x.AccessTime,
                AccessStatus = x.AccessStatus,
                AccessType = x.AccessType,
                DeviceId = x.DeviceId,
                DeviceName = x.DeviceName,
                LeaveStatus = x.LeaveStatus,
                LeaveApplicationId = x.LeaveApplicationId
            });

        return records.Select(x => new NewestPersonnelAccessRecordDto
        {
            PersonnelAccessRecordId = x.Id,
            Name = x.Name,
            Age = x.Age,
            Gender = x.Gender,
            AccessTime = x.AccessTime,
            AccessStatus = x.AccessStatus,
            AccessType = x.AccessType,
            FaceImgPath = x.FaceImgPath ?? x.OriginalFaceUrl,
            SnapImgPath = x.SnapImgPath ?? x.OriginalSnapUrl,
            DeviceId = x.DeviceId,
            DeviceName = x.DeviceName,
            LeaveStatus = x.LeaveStatus,
            LeaveApplicationId = x.LeaveApplicationId
        }).ToList();
    }
}