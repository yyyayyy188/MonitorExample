using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Events;

/// <summary>
///     同步人脸图片事件
/// </summary>
public class SynchronizationImgEvent : DomainEventBase
{
    /// <summary>
    ///     获取事件流Id
    /// </summary>
    public override string StreamId => EventIdentifier.ToString();

    /// <summary>
    ///     人员出入记录事件Id
    /// </summary>
    public Guid EventIdentifier { get; set; }

    /// <summary>
    ///     原始头像图片路径
    /// </summary>
    public string OriginalFaceUrl { get; set; }

    /// <summary>
    ///     原始抓拍图片
    /// </summary>
    public string OriginalSnapUrl { get; set; }

    /// <summary>
    ///     原始面容图片地址
    /// </summary>
    public string RecognitionFaceOssUrl { get; set; }
}