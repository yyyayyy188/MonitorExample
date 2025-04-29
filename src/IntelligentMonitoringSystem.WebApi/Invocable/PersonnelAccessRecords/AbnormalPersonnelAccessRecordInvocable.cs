using Coravel.Invocable;
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.AbnormalLeaveWaring;
using MediatR;

namespace IntelligentMonitoringSystem.WebApi.Invocable.PersonnelAccessRecords;

/// <summary>
///     预警处理
/// </summary>
public class AbnormalPersonnelAccessRecordInvocable(ISender sender) : IInvocable
{
    /// <summary>
    ///     预警处理
    /// </summary>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task Invoke()
    {
        await sender.Send(new AbnormalLeaveWaringCommand());
    }
}