using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Dtos;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;
using IntelligentMonitoringSystem.Domain.Shared.Configs;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using IntelligentMonitoringSystem.Domain.Users;
using Microsoft.Extensions.Options;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Get;

/// <summary>
///     获取人员出入记录详情
/// </summary>
/// <param name="personnelAccessRecordRepository">personnelAccessRecordRepository</param>
/// <param name="options">options</param>
/// <param name="userRepository">userRepository</param>
public class GetPersonnelAccessRecordQueryHandler(
    IBasicRepository<PersonnelAccessRecord> personnelAccessRecordRepository,
    IOptionsMonitor<IntelligentMonitorSystemHttpClientConfig> options,
    IUserRepository userRepository)
    : IQueryHandler<GetPersonnelAccessRecordQuery, PersonnelAccessRecordDetailDto>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>PersonnelAccessRecordDetailDto</returns>
    public async Task<PersonnelAccessRecordDetailDto> Handle(
        GetPersonnelAccessRecordQuery request,
        CancellationToken cancellationToken)
    {
        var personnelAccessRecord = await personnelAccessRecordRepository.GetAsync(request.PersonnelAccessRecordId);
        if (personnelAccessRecord == null)
        {
            throw new UserFriendlyException($"不存在编号为：{request.PersonnelAccessRecordId} 的记录");
        }

        var userAccount = await userRepository.GetUserAccountAsync(personnelAccessRecord.FaceId);
        return new PersonnelAccessRecordDetailDto
        {
            PersonnelAccessRecordId = personnelAccessRecord.Id,
            Name = personnelAccessRecord.Name,
            Age = personnelAccessRecord.Age,
            Gender = personnelAccessRecord.Gender,
            AccessTime = personnelAccessRecord.AccessTime,
            AccessStatus =
                personnelAccessRecord.AccessType == AccessTypeSmartEnum.Enter
                    ? AccessStatusSmartEnum.Normal
                    : personnelAccessRecord.AccessStatus,
            AccessType = personnelAccessRecord.AccessType,
            FaceImgPath =
                personnelAccessRecord.GetFullFaceImgPath(options.CurrentValue) ?? personnelAccessRecord.OriginalFaceUrl,
            SnapImgPath =
                personnelAccessRecord.GetFullSnapImgPath(options.CurrentValue) ?? personnelAccessRecord.OriginalSnapUrl,
            Remark = personnelAccessRecord.Remark,
            VideoPlayback = personnelAccessRecord.GetFullVideoPlayback(options.CurrentValue),
            DeviceId = personnelAccessRecord.DeviceId,
            Nurse = userAccount?.Nurse ?? string.Empty,
            RoomNo = userAccount?.RoomNo ?? string.Empty,
            DeviceName = personnelAccessRecord.DeviceName,
            LeaveStatus = personnelAccessRecord.LeaveStatus,
            LeaveApplicationId = personnelAccessRecord.LeaveApplicationId
        };
    }
}