namespace IntelligentMonitoringSystem.Domain.Shared.Users.Const;

/// <summary>
///     用户常量sql
/// </summary>
public class UserConstSql
{
    /// <summary>
    ///     同步进入记录
    /// </summary>
    public const string SynchronizationEnterAccessInfoSql = """
                                                            update intelligent_monitoring_system.t_user_account
                                                            set latest_enter = @accessTime,
                                                                room_status  = @roomStatus
                                                            where face_id = @faceId
                                                            limit 1;
                                                            """;

    /// <summary>
    ///     同步离开记录
    /// </summary>
    public const string SynchronizationLeaveAccessInfoSql = """
                                                            update intelligent_monitoring_system.t_user_account
                                                            set latest_exit  = @accessTime,
                                                                room_status  = @roomStatus
                                                            where face_id = @faceId
                                                            limit 1;
                                                            """;

    /// <summary>
    ///     同步异常离开次数
    /// </summary>
    public const string SynchronizationAbnormalLeaveAccessInfoSql = """
                                                                    UPDATE intelligent_monitoring_system.t_user_account as user
                                                                    SET outing_times= (select count(1)
                                                                                       from intelligent_monitoring_system.abnormal_personnel_access_record as apar
                                                                                                inner join intelligent_monitoring_system.personnel_access_record par
                                                                                                           on apar.personnel_access_record_id = par.id
                                                                                       where par.face_id = @faceId
                                                                                         AND par.access_status = 'A')
                                                                    WHERE user.face_id = @faceId
                                                                    limit 1;
                                                                    """;

    /// <summary>
    ///     同步离开记录
    /// </summary>
    public const string SynchronizationAbnormalAccessAverageTimeSql = """
                                                                      update intelligent_monitoring_system.t_user_account
                                                                      set average_interval = @averageTime
                                                                      where face_id = @faceId
                                                                      limit 1;
                                                                      """;

    /// <summary>
    ///     获取用户账户信息
    /// </summary>
    public const string GetUserAccountSql = """
                                            select user_id                    as UserId,
                                                   create_time                as CreateTime,
                                                   email                      as Email,
                                                   emergency_contact          as EmergencyContact,
                                                   emergency_contact_phone    as EmergencyContactPhone,
                                                   emergency_contact_relation as EmergencyContactRelation,
                                                   is_admin                   as IsAdmin,
                                                   is_first_login             as IsFirstLogin,
                                                   password                   as Password,
                                                   telephone                  as Telephone,
                                                   user_account               as UserAccount,
                                                   user_name                  as UserName,
                                                   face_id                    as FaceId,
                                                   nurse                      as Nurse,
                                                   room_no                    as RoomNo,
                                                   latest_enter               as LatestEnter,
                                                   latest_exit                as LatestExit,
                                                   room_status                as RoomStatus,
                                                   average_interval           as AverageInterval,
                                                   outing_times               as OutingTimes
                                            from intelligent_monitoring_system.t_user_account
                                            where face_id = @faceId
                                            limit 1;
                                            """;
}