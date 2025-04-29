using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;

/// <summary>
///     异常出入记录仓储
/// </summary>
public interface IAbnormalPersonnelAccessRecordRepository
{
    /// <summary>
    ///     查询告警记录
    /// </summary>
    /// <param name="waringTime">告警时间</param>
    /// <returns>异常出入集合</returns>
    Task<List<AbnormalPersonnelAccessRecord>> QueryWaringRecordAsync(DateTime waringTime);

    /// <summary>
    ///     获取异常出入记录
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>AbnormalPersonnelAccessRecord</returns>
    Task<AbnormalPersonnelAccessRecord> GetAbnormalPersonnelAccessRecordAsync(int id);

    /// <summary>
    ///     分页查询
    /// </summary>
    /// <param name="pageSize">页面大小</param>
    /// <param name="skipCount">跳过行数</param>
    /// <param name="name">姓名</param>
    /// <param name="status">状态</param>
    /// <param name="leaveStartTime">离开的开始时间</param>
    /// <param name="leaveEndTime">离开的结束时间</param>
    /// <param name="enterStartTime">进入的开始时间</param>
    /// <param name="enterEndTime">进入的结束时间</param>
    /// <returns>List<AbnormalPersonnelAccessRecord /></returns>
    Task<List<AbnormalPersonnelAccessRecord>> PageSearchAsync(int pageSize, int skipCount, string name,
        AbnormalPersonnelAccessRecordStatusSmartEnum status, DateTime? leaveStartTime, DateTime? leaveEndTime,
        DateTime? enterStartTime,
        DateTime? enterEndTime);

    /// <summary>
    ///     查询
    /// </summary>
    /// <param name="name">姓名</param>
    /// <param name="status">状态</param>
    /// <param name="leaveStartTime">离开的开始时间</param>
    /// <param name="leaveEndTime">离开的结束时间</param>
    /// <param name="enterStartTime">进入的开始时间</param>
    /// <param name="enterEndTime">进入的结束时间</param>
    /// <param name="ids">查询指定Id集合</param>
    /// <returns>IEnumerable<AbnormalPersonnelAccessRecord /></returns>
    Task<IEnumerable<AbnormalPersonnelAccessRecord>> SearchAsync(string name, AbnormalPersonnelAccessRecordStatusSmartEnum status,
        DateTime? leaveStartTime, DateTime? leaveEndTime, DateTime? enterStartTime,
        DateTime? enterEndTime, List<int> ids);

    /// <summary>
    ///     分页数量统计
    /// </summary>
    /// <param name="name">姓名</param>
    /// <param name="status">状态</param>
    /// <param name="leaveStartTime">离开的开始时间</param>
    /// <param name="leaveEndTime">离开的结束时间</param>
    /// <param name="enterStartTime">进入的开始时间</param>
    /// <param name="enterEndTime">进入的结束时间</param>
    /// <returns>List<AbnormalPersonnelAccessRecord /></returns>
    Task<int> SearchTotalCountAsync(string name, AbnormalPersonnelAccessRecordStatusSmartEnum status,
        DateTime? leaveStartTime, DateTime? leaveEndTime, DateTime? enterStartTime, DateTime? enterEndTime);

    /// <summary>
    ///     保存未按时返回的数据值异常记录表并更新原始状态
    /// </summary>
    /// <param name="eventIdentifiers">事件Id</param>
    /// <param name="nextWaringTime">下次告警时间</param>
    /// <returns>Task</returns>
    Task SaveNotReturnOnTimeRecordAsync(List<Guid> eventIdentifiers, DateTime nextWaringTime);

    /// <summary>
    ///     获取异常外出平均时间
    /// </summary>
    /// <param name="faceId">面容Id</param>
    /// <returns>平均时长</returns>
    Task<long> GetAbnormalAccessAverageTimeAsync(string faceId);

    /// <summary>
    ///     更新异常外出记录下次告警时间
    /// </summary>
    /// <param name="ids">ids</param>
    /// <param name="nextWaringTime">下次告警时间</param>
    /// <returns>Task</returns>
    Task UpdateAbnormalPersonnelAccessRecordNextWaring(List<int> ids, DateTime nextWaringTime);
}