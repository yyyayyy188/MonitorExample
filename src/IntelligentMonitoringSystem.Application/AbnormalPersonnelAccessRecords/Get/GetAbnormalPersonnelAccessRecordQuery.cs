using IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Dtos;
using IntelligentMonitoringSystem.Application.Common.Contracts;

namespace IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Get;

/// <summary>
///     查询异常出入记录详情信息
/// </summary>
public class GetAbnormalPersonnelAccessRecordQuery(int id) : QueryBase<AbnormalPersonnelAccessRecordDetailDto>
{
    /// <summary>
    ///     异常出入记录ID
    /// </summary>
    public new int Id { get; set; } = id;
}