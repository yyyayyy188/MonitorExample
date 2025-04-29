using System.Linq.Expressions;
using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Dtos;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.Extensions;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Export;

/// <summary>
///     导出人员出入记录
/// </summary>
public class ExportPersonnelAccessQueryHandler(IBasicRepository<PersonnelAccessRecord> personnelAccessRecordRepository)
    : IQueryHandler<ExportPersonnelAccessQuery, IEnumerable<ExportPersonnelAccessRecordDto>>
{
    /// <summary>
    ///     导出人员出入记录
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// A
    /// <see cref="Task" />
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    /// representing the asynchronous operation.
    public async Task<IEnumerable<ExportPersonnelAccessRecordDto>> Handle(
        ExportPersonnelAccessQuery request,
        CancellationToken cancellationToken)
    {
        Expression<Func<PersonnelAccessRecord, PersonnelAccessRecord>> expression = selector => new PersonnelAccessRecord
        {
            Id = selector.Id,
            Name = selector.Name,
            Age = selector.Age,
            Gender = selector.Gender,
            AccessStatus = selector.AccessStatus,
            AccessType = selector.AccessType,
            AccessTime = selector.AccessTime,
            LeaveStatus = selector.LeaveStatus,
            LeaveApplicationId = selector.LeaveApplicationId
        };
        var predicate = PredicateBuilder.New<PersonnelAccessRecord>(true);

        if (request.PersonnelAccessRecordIds is not null)
        {
            predicate.And(x => request.PersonnelAccessRecordIds.Contains(x.Id));
        }
        else
        {
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
        }

        var records = await personnelAccessRecordRepository.QueryAsync(
            predicate,
            expression);
        var personnelAccessRecords = records as PersonnelAccessRecord[] ?? records.ToArray();
        if (personnelAccessRecords.Length == 0)
        {
            return new List<ExportPersonnelAccessRecordDto>();
        }

        return personnelAccessRecords.Select(x => new ExportPersonnelAccessRecordDto
        {
            PersonnelAccessRecordId = x.Id,
            AccessStatus = x.AccessStatus.DisplayName,
            AccessType = x.AccessType.DisplayName,
            AccessTime = x.AccessTime.ToString("yyyy-MM-dd HH:mm:ss"),
            Age = x.Age,
            Gender = x.Gender,
            Name = x.Name,
            LeaveStatus = x.LeaveStatus?.DisplayName ?? "--"
        });
    }
}