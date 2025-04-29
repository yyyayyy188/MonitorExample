using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;
using IntelligentMonitoringSystem.Domain.Shared.Configs;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using Microsoft.Extensions.Options;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.GetVideoPlayback;

/// <summary>
///     视频回放
/// </summary>
public class GetVideoPlaybackQueryHandler(
    IBasicRepository<PersonnelAccessRecord> personnelAccessRecordRepository,
    IOptionsMonitor<IntelligentMonitorSystemHttpClientConfig> options)
    : IQueryHandler<GetVideoPlaybackQuery, string?>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>string</returns>
    public async Task<string?> Handle(GetVideoPlaybackQuery request, CancellationToken cancellationToken)
    {
        if (request.PersonnelAccessRecordId <= 0)
        {
            throw new UserFriendlyException("没有匹配的记录");
        }

        var personnelAccessRecord = await personnelAccessRecordRepository.FirstOrDefaultAsync(
            x => x.Id == request.PersonnelAccessRecordId,
            selector: selector => new PersonnelAccessRecord { DevicePlayback = selector.DevicePlayback });
        if (personnelAccessRecord == null)
        {
            throw new UserFriendlyException("没有匹配的记录");
        }

        return personnelAccessRecord.GetFullVideoPlayback(options.CurrentValue);
    }
}