namespace IntelligentMonitoringSystem.Domain.LeaveApplications;

/// <summary>
///     请假记录管理
/// </summary>
public interface ILeaveApplicationManage
{
    /// <summary>
    ///     根据人脸编号获取请假记录
    /// </summary>
    /// <param name="faceId">面容Id</param>
    /// <returns>LeaveApplication</returns>
    Task<LeaveApplication> GetLeaveApplicationByFaceIdAsync(string faceId);
}