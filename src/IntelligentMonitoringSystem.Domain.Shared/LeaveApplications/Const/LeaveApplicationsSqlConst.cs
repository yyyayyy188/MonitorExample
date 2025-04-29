namespace IntelligentMonitoringSystem.Domain.Shared.LeaveApplications.Const;

/// <summary>
///     请假申请Sql
/// </summary>
public static class LeaveApplicationsSqlConst
{
    /// <summary>
    ///     根据人脸ID获取请假申请
    /// </summary>
    public const string GetLeaveApplicationByFaceIdSql = """
                                                         select
                                                         @faceId              AS FaceId,
                                                         l.application_status AS ApplicationStatus,
                                                         l.start_time         AS StartTime,
                                                         l.end_time           AS EndTime,
                                                         l.id                 as Id,
                                                         l.approval_time      as ApprovalTime,
                                                         l.user_id            as UserId
                                                         from intelligent_monitoring_system.t_user_account as u
                                                         left join intelligent_monitoring_system.t_leave_application as l
                                                         on u.user_id = l.user_id
                                                         where u.face_id = @faceId
                                                         and l.start_time between @beginTime and @endTime
                                                         order by l.approval_time desc,l.id desc
                                                         limit 1;
                                                         """;
}