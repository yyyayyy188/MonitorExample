using Ardalis.SmartEnum.JsonNet;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Dtos;

/// <summary>
///     更新人员离开通行记录的请假状态
/// </summary>
public class UpdateLeaveAccessStatusDto
{
    /// <summary>
    ///     出入状态
    /// </summary>
    [JsonProperty("accessStatus")]
    [JsonConverter(typeof(SmartEnumNameConverter<AccessStatusSmartEnum, string>))]
    public AccessStatusSmartEnum AccessStatus { get; set; }

    /// <summary>
    ///     备注信息
    /// </summary>
    [JsonProperty("remark")]
    public string? Remark { get; set; }
}