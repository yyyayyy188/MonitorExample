namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;

/// <summary>
///     文件仓储
/// </summary>
public interface IPersonnelAccessRecordFileRepository
{
    /// <summary>
    ///     上传文件
    /// </summary>
    /// <param name="originalFileUrl">originalFileUrl</param>
    /// <returns>string</returns>
    Task<string> UploadFile(string originalFileUrl);

    /// <summary>
    ///     上传视频
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <param name="fileName">文件名</param>
    /// <returns>文件地址</returns>
    Task<string> UploadVideo(Stream stream, string fileName);
}