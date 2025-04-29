using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.MonitoringCenters.Dtos;

/// <summary>
///     人员出入记录统计信息
/// </summary>
public class PersonnelAccessRecordStatisticsDto
{
    /// <summary>
    ///     总数量
    /// </summary>
    [JsonProperty("totalCount")]
    public int TotalCount { get; set; }

    /// <summary>
    ///     进入数量
    /// </summary>
    [JsonProperty("enterCount")]
    public int EnterCount { get; set; }

    /// <summary>
    ///     离开数量
    /// </summary>
    [JsonProperty("leaveCount")]
    public int LeaveCount { get; set; }

    /// <summary>
    ///     异常离开数量
    /// </summary>
    [JsonProperty("abnormalLeaveCount")]
    public int AbnormalLeaveCount { get; set; }
}