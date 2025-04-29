using Ardalis.GuardClauses;
using IntelligentMonitoringSystem.Application.Shared.MessageCenters.Enum;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.MessageCenters.MessageDataResolvers.Dtos;

/// <summary>
///     通知数据基类
/// </summary>
public class BaseMessageDto(MessageCenterNotifyType notifyType)
{
    /// <summary>
    ///     通知类型 离开=0,进入=1,异常追踪=2
    /// </summary>
    [JsonProperty("notifyType")]
    public MessageCenterNotifyType NotifyType { get; set; } = Guard.Against.Null(notifyType);
}