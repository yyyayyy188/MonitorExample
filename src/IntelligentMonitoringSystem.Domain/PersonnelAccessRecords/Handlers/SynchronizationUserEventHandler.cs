using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Events;
using IntelligentMonitoringSystem.Domain.Users;
using MediatR;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Handlers;

/// <summary>
///     同步用户
/// </summary>
public class SynchronizationUserEventHandler(IUserRepository userRepository)
    : INotificationHandler<SynchronizationUserEvent>
{
    /// <summary>
    ///     同步信息至用户
    /// </summary>
    /// <param name="notification">notification</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task Handle(SynchronizationUserEvent notification, CancellationToken cancellationToken)
    {
        await userRepository.SynchronizationAccessInfoAsync(notification.FaceId, notification.AccessTime,
            notification.AccessType);
    }
}