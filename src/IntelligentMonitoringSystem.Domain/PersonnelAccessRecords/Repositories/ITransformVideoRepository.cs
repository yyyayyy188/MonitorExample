namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;

/// <summary>
///     视频转码
/// </summary>
public interface ITransformVideoRepository
{
    /// <summary>
    ///     转码视频
    /// </summary>
    /// <param name="videoPath">视频路径</param>
    /// <param name="temporaryFilePath">临时文件路径</param>
    /// <returns>流</returns>
    Task<bool> TransformVideoAsync(string videoPath, string temporaryFilePath);
}