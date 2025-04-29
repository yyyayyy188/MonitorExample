using IntelligentMonitoringSystem.Application.Common.Contracts;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.GetVideoPlayback;

/// <summary>
///     视频回放
/// </summary>
public class GetVideoPlaybackQuery : IQuery<string?>
{
    /// <summary>
    ///     人员通行记录Id
    /// </summary>
    public int PersonnelAccessRecordId { get; set; }
}