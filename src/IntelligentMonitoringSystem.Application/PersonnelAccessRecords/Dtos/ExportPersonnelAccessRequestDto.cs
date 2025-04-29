using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Dtos;

/// <summary>
///     导出出入记录请求
/// </summary>
public class ExportPersonnelAccessRequestDto
{
    /// <summary>
    ///     姓名
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>
    ///     出入状态 Abnormal:异常 Normal:正常 Pending:暂无
    /// </summary>
    [JsonProperty("accessStatus")]
    public string? AccessStatus { get; set; }

    /// <summary>
    ///     开始事件
    /// </summary>
    [JsonProperty("startTime")]
    public DateTime? StartTime { get; set; }

    /// <summary>
    ///     结束时间
    /// </summary>
    [JsonProperty("endTime")]
    public DateTime? EndTime { get; set; }

    /// <summary>
    ///     出入记录编号
    /// </summary>
    [JsonProperty("personnelAccessRecordIds")]
    public List<int>? PersonnelAccessRecordIds { get; set; }
}