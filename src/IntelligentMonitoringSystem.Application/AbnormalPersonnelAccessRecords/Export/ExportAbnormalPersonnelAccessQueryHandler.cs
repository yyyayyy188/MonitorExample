using IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Dtos;
using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;

namespace IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Export;

/// <summary>
///     导出异常出入记录
/// </summary>
/// <param name="abnormalPersonnelAccessRecordRepository">abnormalPersonnelAccessRecordRepository</param>
public class ExportAbnormalPersonnelAccessQueryHandler(
    IAbnormalPersonnelAccessRecordRepository abnormalPersonnelAccessRecordRepository)
    : IQueryHandler<ExportAbnormalPersonnelAccessQuery, IEnumerable<ExportAbnormalPersonnelAccessDto>>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>PersonnelAccessRecordDetailDto</returns>
    public async Task<IEnumerable<ExportAbnormalPersonnelAccessDto>> Handle(
        ExportAbnormalPersonnelAccessQuery request, CancellationToken cancellationToken)
    {
        var records = await abnormalPersonnelAccessRecordRepository.SearchAsync(request.Name, request.Status,
            request.LeaveStartTime,
            request.LeaveEndTime,
            request.EnterStartTime, request.EnterEndTime, request.Ids);
        var personnelAccessRecords = records as AbnormalPersonnelAccessRecord[] ?? records.ToArray();
        if (personnelAccessRecords.Length == 0)
        {
            return new List<ExportAbnormalPersonnelAccessDto>();
        }

        return personnelAccessRecords.Select(x => new ExportAbnormalPersonnelAccessDto
        {
            Id = x.Id,
            Name = x.PersonnelAccessRecord.Name,
            Age = x.PersonnelAccessRecord.Age,
            Gender = x.PersonnelAccessRecord.Gender,
            Status = x.Status.DisplayName,
            LeaveTime = x.LeaveTime.ToString("yyyy-MM-dd HH:mm:ss"),
            ReturnTime = x.ReturnTime.HasValue ? x.ReturnTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty,
            Nurse = x.PersonnelAccessRecord.Nurse,
            RoomNo = x.PersonnelAccessRecord.RoomNo,
            LeaveStatus = x.PersonnelAccessRecord.LeaveStatus?.DisplayName ?? "--"
        });
    }
}