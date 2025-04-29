namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;

/// <summary>
///     视频仓储
/// </summary>
public interface IPersonnelAccessRecordVideoRepository
{
    /// <summary>
    ///     获取视频
    /// </summary>
    /// <param name="deviceId">设备Id</param>
    /// <param name="accessTime">访问时间</param>
    /// <returns>视频地址</returns>
    Task<string> GetVideoAsync(string deviceId, DateTime accessTime);
}