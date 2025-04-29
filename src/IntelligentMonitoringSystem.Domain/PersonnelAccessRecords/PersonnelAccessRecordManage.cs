using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Exception;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;

/// <summary>
///     人员识别记录管理
/// </summary>
/// <param name="basicRepository">basicRepository</param>
/// <param name="topicAlarmEventLogRepository">topicAlarmEventLogRepository</param>
public class PersonnelAccessRecordManage(
    IBasicRepository<PersonnelAccessRecord> basicRepository,
    IBasicRepository<TopicAlarmEventLog> topicAlarmEventLogRepository)
    : IPersonnelAccessRecordManage
{
    /// <summary>
    ///     保存人员识别记录
    /// </summary>
    /// <param name="personnelAccessRecord">personnelAccessRecord.</param>
    /// <returns>Task{bool}</returns>
    public async Task<bool> SaveAsync(PersonnelAccessRecord personnelAccessRecord)
    {
        var entity = await basicRepository.InsertAsync(personnelAccessRecord);
        entity.SetPersonnelAccessRecordId();
        await topicAlarmEventLogRepository.InsertAsync(entity.TopicAlarmEventLog);
        return true;
    }

    /// <summary>
    ///     同步图片
    /// </summary>
    /// <param name="eventIdentifier">事件标识</param>
    /// <param name="faceImgPath">面部图片地址</param>
    /// <param name="snapImgPath">抓拍图片地址</param>
    /// <param name="recognitionFacePath">原始采集图片地址</param>
    /// <returns>bool</returns>
    public async Task<bool> SynchronizationImgAsync(Guid eventIdentifier, string faceImgPath,
        string snapImgPath, string recognitionFacePath)
    {
        var entity = new PersonnelAccessRecord(eventIdentifier, faceImgPath, snapImgPath);
        await basicRepository.ExecuteUpdateAsync(x => x.EventIdentifier == entity.EventIdentifier, setters => setters
            .SetProperty(b => b.FaceImgPath, entity.FaceImgPath)
            .SetProperty(b => b.SnapImgPath, entity.SnapImgPath)
            .SetProperty(b => b.RecognitionFacePath, recognitionFacePath));
        return true;
    }

    /// <summary>
    ///     根据事件标识获取人员出入记录
    /// </summary>
    /// <param name="eventIdentifier">eventIdentifier</param>
    /// <returns>PersonnelAccessRecord</returns>
    public async Task<PersonnelAccessRecord> GetPersonnelAccessRecordByEventIdentifierIdAsync(Guid eventIdentifier)
    {
        return await basicRepository.FirstOrDefaultAsync(
            x => x.EventIdentifier == eventIdentifier,
            selector: x => new PersonnelAccessRecord
            {
                Id = x.Id,
                Name = x.Name,
                Age = x.Age,
                Gender = x.Gender,
                AccessStatus = x.AccessStatus,
                EventIdentifier = x.EventIdentifier,
                LeaveStatus = x.LeaveStatus,
                DeviceId = x.DeviceId,
                DeviceName = x.DeviceName,
                LeaveApplicationId = x.LeaveApplicationId,
                OriginalFaceUrl = x.OriginalFaceUrl,
                OriginalSnapUrl = x.OriginalSnapUrl,
                FaceImgPath = x.FaceImgPath,
                SnapImgPath = x.SnapImgPath,
                AccessType = x.AccessType,
                AccessTime = x.AccessTime
            });
    }

    /// <summary>
    ///     更新出入记录状态
    /// </summary>
    /// <param name="personnelAccessRecordId">出入记录Id</param>
    /// <param name="accessStatus">出入状态</param>
    /// <param name="remark">备注</param>
    /// <param name="userId">用户Id</param>
    /// <returns>bool</returns>
    public async Task<bool> UpdateLeaveAccessStatusAsync(
        int personnelAccessRecordId, AccessStatusSmartEnum accessStatus, string remark, string userId)
    {
        var personnelAccessRecord = await basicRepository.FirstOrDefaultAsync(
            x => x.Id == personnelAccessRecordId,
            selector: selector => new PersonnelAccessRecord
            {
                Id = selector.Id,
                AccessStatus = selector.AccessStatus,
                AccessTime = selector.AccessTime,
                FaceId = selector.FaceId,
                AccessType = selector.AccessType
            });
        if (personnelAccessRecord == null)
        {
            throw new NotFoundPersonnelAccessRecordException();
        }

        personnelAccessRecord.SetLeaveAccessStatus(accessStatus, remark, userId);
        await basicRepository.UpdateAsync(
            new PersonnelAccessRecord(personnelAccessRecord.Id), action =>
            {
                action.AccessStatus = personnelAccessRecord.AccessStatus;
                action.Remark = personnelAccessRecord.Remark;
                action.LastEditUserId = personnelAccessRecord.LastEditUserId;
                action.LastEditTime = personnelAccessRecord.LastEditTime;
                action.LastEditUserName = personnelAccessRecord.LastEditUserName;
                action.AddEvents(personnelAccessRecord.GetEvents());
            });
        return true;
    }

    /// <summary>
    ///     更新进入记录状态
    /// </summary>
    /// <param name="personnelAccessRecordId">出入记录Id</param>
    /// <param name="remark">备注</param>
    /// <param name="userId">用户Id</param>
    /// <returns>bool</returns>
    public async Task<bool> UpdateEnterAccessStatusAsync(int personnelAccessRecordId, string remark, string userId)
    {
        var isExist = await basicRepository.IsExistAsync(x => x.Id == personnelAccessRecordId);
        if (!isExist)
        {
            throw new NotFoundPersonnelAccessRecordException();
        }

        var personnelAccessRecord = new PersonnelAccessRecord { Id = personnelAccessRecordId };
        personnelAccessRecord.SetEnterAccessStatus(remark, userId);
        await basicRepository.UpdateAsync(new PersonnelAccessRecord(personnelAccessRecordId), action =>
        {
            action.Remark = personnelAccessRecord.Remark;
            action.LastEditUserId = personnelAccessRecord.LastEditUserId;
            action.LastEditTime = personnelAccessRecord.LastEditTime;
            action.LastEditUserName = personnelAccessRecord.LastEditUserName;
        });
        return true;
    }
}