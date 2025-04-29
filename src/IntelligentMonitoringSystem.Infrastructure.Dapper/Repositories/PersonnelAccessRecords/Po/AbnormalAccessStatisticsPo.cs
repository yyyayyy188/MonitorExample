namespace IntelligentMonitoringSystem.Infrastructure.Dapper.Repositories.PersonnelAccessRecords.Po;

/// <summary>
///     异常通行记录统计
/// </summary>
public class AbnormalAccessStatisticsPo
{
    /// <summary>
    ///     人脸ID
    /// </summary>
    public string FaceId { get; set; }

    /// <summary>
    ///     统计数量
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    ///     总时长
    /// </summary>
    public long TotalTime { get; set; }
}