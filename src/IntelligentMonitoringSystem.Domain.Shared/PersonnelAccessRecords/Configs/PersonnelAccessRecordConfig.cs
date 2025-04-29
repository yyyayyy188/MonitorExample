namespace IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Configs;

/// <summary>
///     考勤记录配置
/// </summary>
public class PersonnelAccessRecordConfig
{
    /// <summary>
    ///     位置
    /// </summary>
    public const string PositionOptions = "PersonnelAccessRecordConfig";

    /// <summary>
    ///     告警间隔秒数
    /// </summary>
    public int? WaringIntervalSecond { get; set; }

    /// <summary>
    ///     文件服务器地址
    /// </summary>
    public string FileServerHost { get; set; }
}