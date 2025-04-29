using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.MonitoringCenters.Dtos;

namespace IntelligentMonitoringSystem.Application.MonitoringCenters.NewestRecord;

/// <summary>
///     最新出入记录信息
/// </summary>
public class NewestRecordQuery : QueryBase<List<NewestPersonnelAccessRecordDto>>
{
    /// <summary>
    ///     查询记录数
    /// </summary>
    public int Size { get; set; }
}