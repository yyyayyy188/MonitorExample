using IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Dtos;
using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.Configs;
using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using IntelligentMonitoringSystem.Domain.Users;
using Microsoft.Extensions.Options;

namespace IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Get;

/// <summary>
///     获取异常人员出入记录详情
/// </summary>
/// <param name="abnormalPersonnelAccessRecordRepository">abnormalPersonnelAccessRecordRepository</param>
/// <param name="options">options</param>
/// <param name="userRepository">userRepository</param>
public class GetAbnormalPersonnelAccessRecordQueryHandler(
    IAbnormalPersonnelAccessRecordRepository abnormalPersonnelAccessRecordRepository,
    IOptionsMonitor<IntelligentMonitorSystemHttpClientConfig> options,
    IUserRepository userRepository)
    : IQueryHandler<GetAbnormalPersonnelAccessRecordQuery,
        AbnormalPersonnelAccessRecordDetailDto>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>PersonnelAccessRecordDetailDto</returns>
    public async Task<AbnormalPersonnelAccessRecordDetailDto> Handle(
        GetAbnormalPersonnelAccessRecordQuery request,
        CancellationToken cancellationToken)
    {
        var abnormalPersonnelAccessRecord =
            await abnormalPersonnelAccessRecordRepository.GetAbnormalPersonnelAccessRecordAsync(request.Id);
        if (abnormalPersonnelAccessRecord == null)
        {
            throw new UserFriendlyException($"不存在编号为：{request.Id} 的记录");
        }

        var userAccount = await userRepository.GetUserAccountAsync(abnormalPersonnelAccessRecord.FaceId);
        return new AbnormalPersonnelAccessRecordDetailDto
        {
            Id = abnormalPersonnelAccessRecord.Id,
            PersonnelAccessRecordId = abnormalPersonnelAccessRecord.PersonnelAccessRecordId,
            Name = abnormalPersonnelAccessRecord.PersonnelAccessRecord.Name,
            Age = abnormalPersonnelAccessRecord.PersonnelAccessRecord.Age,
            Gender = abnormalPersonnelAccessRecord.PersonnelAccessRecord.Gender,
            LeaveTime = abnormalPersonnelAccessRecord.LeaveTime,
            ReturnTime = abnormalPersonnelAccessRecord.ReturnTime,
            Status = abnormalPersonnelAccessRecord.Status,
            FaceImgPath =
                abnormalPersonnelAccessRecord.PersonnelAccessRecord.GetFullFaceImgPath(options.CurrentValue) ??
                abnormalPersonnelAccessRecord.PersonnelAccessRecord.OriginalFaceUrl,
            SnapImgPath =
                abnormalPersonnelAccessRecord.PersonnelAccessRecord.GetFullSnapImgPath(options.CurrentValue) ??
                abnormalPersonnelAccessRecord.PersonnelAccessRecord.OriginalSnapUrl,
            Remark = abnormalPersonnelAccessRecord.PersonnelAccessRecord.Remark,
            VideoPlayback = abnormalPersonnelAccessRecord.PersonnelAccessRecord.GetFullVideoPlayback(options.CurrentValue),
            Nurse = userAccount?.Nurse ?? string.Empty,
            RoomNo = userAccount?.RoomNo ?? string.Empty,
            DeviceId = abnormalPersonnelAccessRecord.PersonnelAccessRecord.DeviceId,
            DeviceName = abnormalPersonnelAccessRecord.PersonnelAccessRecord.DeviceName,
            TotalTimes = (long)((abnormalPersonnelAccessRecord.ReturnTime ?? DateTime.Now) -
                                abnormalPersonnelAccessRecord.LeaveTime).TotalSeconds
        };
    }
}