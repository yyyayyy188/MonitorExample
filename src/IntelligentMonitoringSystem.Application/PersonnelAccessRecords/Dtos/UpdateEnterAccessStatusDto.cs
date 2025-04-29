using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Dtos;

/// <summary>
///     更新进入门禁状态
/// </summary>
public class UpdateEnterAccessStatusDto
{
    /// <summary>
    ///     备注信息
    /// </summary>
    [JsonProperty("remark")]
    public string? Remark { get; set; }
}