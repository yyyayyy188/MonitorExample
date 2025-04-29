using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.UpdateLeave;

/// <summary>
///     更新出入状态
/// </summary>
/// <param name="personnelAccessRecordManage">personnelAccessRecordManage</param>
public class UpdateAccessStatusCommandHandler(IPersonnelAccessRecordManage personnelAccessRecordManage)
    : ICommandHandler<UpdateLeaveAccessStatusCommand>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task Handle(UpdateLeaveAccessStatusCommand request, CancellationToken cancellationToken)
    {
        await personnelAccessRecordManage.UpdateLeaveAccessStatusAsync(request.PersonnelAccessRecordId, request.AccessStatus,
            request.Remark, request.UserId);
    }
}