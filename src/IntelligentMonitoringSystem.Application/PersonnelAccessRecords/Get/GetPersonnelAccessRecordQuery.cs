using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Dtos;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Get;

/// <summary>
///     获取人员出入记录详情
/// </summary>
public class GetPersonnelAccessRecordQuery : QueryBase<PersonnelAccessRecordDetailDto>
{
    /// <summary>
    ///     人员出入记录ID
    /// </summary>
    public int PersonnelAccessRecordId { get; set; }
}