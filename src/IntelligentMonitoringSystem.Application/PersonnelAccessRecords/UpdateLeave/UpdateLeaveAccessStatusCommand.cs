using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.UpdateLeave;

/// <summary>
///     更新离开访问状态
/// </summary>
public class UpdateLeaveAccessStatusCommand : CommandBase
{
    /// <summary>
    ///     访问状态
    /// </summary>
    public AccessStatusSmartEnum AccessStatus { get; set; }

    /// <summary>
    ///     备注信息
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    ///     出入记录单Id
    /// </summary>
    public int PersonnelAccessRecordId { get; set; }

    /// <summary>
    ///     操作用户Id
    /// </summary>
    public string UserId { get; set; }
}