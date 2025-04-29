using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;

/// <summary>
///     人员出入记录管理
/// </summary>
public interface IPersonnelAccessRecordManage
{
    /// <summary>
    ///     保存人员出入记录
    /// </summary>
    /// <param name="personnelAccessRecord">personnelAccessRecord</param>
    /// <returns>bool</returns>
    Task<bool> SaveAsync(PersonnelAccessRecord personnelAccessRecord);

    /// <summary>
    ///     同步图片
    /// </summary>
    /// <param name="eventIdentifier">事件标识</param>
    /// <param name="faceImgPath">面部图片地址</param>
    /// <param name="snapImgPath">抓拍图片地址</param>
    /// <param name="recognitionFacePath">原始采集图片地址</param>
    /// <returns>bool</returns>
    Task<bool> SynchronizationImgAsync(Guid eventIdentifier, string faceImgPath,
        string snapImgPath, string recognitionFacePath);

    /// <summary>
    ///     根据事件标识获取人员出入记录
    /// </summary>
    /// <param name="eventIdentifier">eventIdentifier</param>
    /// <returns>PersonnelAccessRecord</returns>
    Task<PersonnelAccessRecord> GetPersonnelAccessRecordByEventIdentifierIdAsync(Guid eventIdentifier);

    /// <summary>
    ///     更新离开记录状态
    /// </summary>
    /// <param name="personnelAccessRecordId">出入记录Id</param>
    /// <param name="accessStatus">出入状态</param>
    /// <param name="remark">备注</param>
    /// <param name="userId">用户Id</param>
    /// <returns>bool</returns>
    Task<bool> UpdateLeaveAccessStatusAsync(int personnelAccessRecordId, AccessStatusSmartEnum accessStatus, string remark,
        string userId);

    /// <summary>
    ///     更新进入记录状态
    /// </summary>
    /// <param name="personnelAccessRecordId">出入记录Id</param>
    /// <param name="remark">备注</param>
    /// <param name="userId">用户Id</param>
    /// <returns>bool</returns>
    Task<bool> UpdateEnterAccessStatusAsync(int personnelAccessRecordId, string remark, string userId);
}