using IntelligentMonitoringSystem.Application.Common.Contracts;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.UpdateEnter;

/// <summary>
///     更新入门记录状态
/// </summary>
public class UpdateEnterAccessStatusCommand : CommandBase
{
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