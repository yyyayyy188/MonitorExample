#nullable enable
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;

namespace IntelligentMonitoringSystem.Domain.Users;

/// <summary>
///     用户仓储
/// </summary>
public interface IUserRepository
{
    /// <summary>
    ///     同步异常访问次数
    /// </summary>
    /// <param name="faceId">faceId</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task SynchronizationAbnormalLeaveAccessInfoAsync(string faceId);

    /// <summary>
    ///     同步用户信息
    /// </summary>
    /// <param name="faceId">面容Id</param>
    /// <param name="accessTime">访问时间</param>
    /// <param name="accessType">访问类型</param>
    /// <returns>Task</returns>
    Task SynchronizationAccessInfoAsync(string faceId, DateTime accessTime, AccessTypeSmartEnum accessType);

    /// <summary>
    ///     同步异常访问平均时长
    /// </summary>
    /// <param name="faceId">面容Id</param>
    /// <param name="averageTime">平均时长</param>
    /// <returns>Task</returns>
    Task SynchronizationAbnormalAccessAverageTimeAsync(string faceId, long averageTime);

    /// <summary>
    ///     获取用户账户信息
    /// </summary>
    /// <param name="faceId">面容Id</param>
    /// <returns>UserAccount</returns>
    Task<UserAccount?> GetUserAccountAsync(string faceId);
}