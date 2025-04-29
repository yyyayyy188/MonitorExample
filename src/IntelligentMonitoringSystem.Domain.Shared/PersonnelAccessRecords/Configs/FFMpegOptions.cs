namespace IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Configs;

/// <summary>
///     FFMpeg配置
/// </summary>
public class FFMpegOptions
{
    /// <summary>
    ///     FFMpeg安装路径
    /// </summary>
    public string BinaryFolder { get; set; }

    /// <summary>
    ///     视频文件保存路径
    /// </summary>
    public string BaseVideoPath { get; set; }
}