using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.LeaveApplications.Const;

namespace IntelligentMonitoringSystem.Domain.LeaveApplications;

/// <summary>
///     请假记录管理
/// </summary>
/// <param name="readOnlyBasicRepository">readOnlyBasicRepository</param>
public class LeaveApplicationManage(IReadOnlyBasicRepository<LeaveApplication> readOnlyBasicRepository)
    : ILeaveApplicationManage
{
    /// <summary>
    ///     根据人脸编号获取请假记录
    /// </summary>
    /// <param name="faceId">面容Id</param>
    /// <returns>LeaveApplication</returns>
    public async Task<LeaveApplication> GetLeaveApplicationByFaceIdAsync(string faceId)
    {
        return await readOnlyBasicRepository.QueryFirstOrDefaultAsync(
            LeaveApplicationsSqlConst.GetLeaveApplicationByFaceIdSql,
            new { faceId, beginTime = DateTime.Now.Date, endTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1) });
    }
}