using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.UpdateEnter;

/// <summary>
///     更新进入门禁状态
/// </summary>
/// <param name="personnelAccessRecordManage">personnelAccessRecordManage</param>
public class UpdateEnterAccessStatusCommandHandler(IPersonnelAccessRecordManage personnelAccessRecordManage)
    : ICommandHandler<UpdateEnterAccessStatusCommand>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task Handle(UpdateEnterAccessStatusCommand request, CancellationToken cancellationToken)
    {
        await personnelAccessRecordManage.UpdateEnterAccessStatusAsync(request.PersonnelAccessRecordId, request.Remark,
            request.UserId);
    }
}