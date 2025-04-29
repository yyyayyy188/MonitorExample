using Ardalis.GuardClauses;
using IntelligentMonitoringSystem.Application.MessageCenters.MessageDataResolvers.Dtos;
using IntelligentMonitoringSystem.Application.Shared.Dto;
using IntelligentMonitoringSystem.Application.Shared.MessageCenters.Enum;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;
using IntelligentMonitoringSystem.Domain.Shared.Configs;
using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using IntelligentMonitoringSystem.Domain.Shared.MessageCenters.Enums;
using Microsoft.Extensions.Options;

namespace IntelligentMonitoringSystem.Application.MessageCenters.MessageDataResolvers.Impl;

/// <summary>
///     出入记录消息通知解析器
/// </summary>
public class AccessRecordMessageDataResolver(
    IPersonnelAccessRecordManage personnelAccessRecordManage,
    IOptionsMonitor<IntelligentMonitorSystemHttpClientConfig> options)
    : IMessageDataResolver
{
    /// <summary>
    ///     数据解析
    /// </summary>
    /// <param name="eventSource">事件源</param>
    /// <param name="data">data</param>
    /// <returns>string</returns>
    public async Task<object> DataResolver(EventSourceSmartEnum eventSource, Dictionary<string, object> data)
    {
        Guard.Against.Null(data);
        if (!data.TryGetValue("EventIdentifier", out var eventIdentifier))
        {
            throw new UserFriendlyException("未查询到记录信息，无法解析参数 EventIdentifier");
        }

        var entity =
            await personnelAccessRecordManage.GetPersonnelAccessRecordByEventIdentifierIdAsync(
                Guid.Parse(eventIdentifier.ToString() ?? string.Empty));
        if (entity == null)
        {
            throw new UserFriendlyException("为查询到记录信息");
        }

        var accessRecordNotificationResponseDto =
            new AccessRecordNotificationResponseDto(
                eventSource == EventSourceSmartEnum.Enter || eventSource == EventSourceSmartEnum.AbnormalEnter
                    ? MessageCenterNotifyType.Enter
                    : MessageCenterNotifyType.Leave)
            {
                PersonnelAccessRecordId = entity.Id,
                AccessTime = entity.AccessTime.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessType = entity.AccessType,
                Age = entity.Age,
                DeviceName = entity.DeviceName,
                FaceImgUrl = entity.GetFullFaceImgPath(options.CurrentValue) ?? entity.OriginalFaceUrl,
                Gender = entity.Gender,
                LeaveApplicationId = entity.LeaveApplicationId,
                LeaveStatus = entity.LeaveStatus,
                Name = entity.Name,
                SnapImgPath = entity.GetFullSnapImgPath(options.CurrentValue) ?? entity.OriginalSnapUrl
            };
        return ResponseEntity<AccessRecordNotificationResponseDto>.Ok(accessRecordNotificationResponseDto);
    }
}