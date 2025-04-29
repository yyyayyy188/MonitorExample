namespace IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Const;

/// <summary>
///     出入记录sql
/// </summary>
public static class PersonnelAccessRecordSqlConst
{
       /// <summary>
       ///     查询月度异常统计
       /// </summary>
       public const string QueryMonthAbnormalRecordStatisticsSql = """
                                                                    select distinct date(leave_time) as LeaveTime
                                                                   from intelligent_monitoring_system.abnormal_personnel_access_record
                                                                   where leave_time between @beginTime and @endTime
                                                                   """;

       /// <summary>
       ///     查询告警通知记录
       /// </summary>
       public const string QueryWaringRecordSql = """
                                                   select ab.id                         as Id,
                                                         ab.personnel_access_record_id as PersonnelAccessRecordId
                                                  from intelligent_monitoring_system.abnormal_personnel_access_record as ab
                                                           inner join intelligent_monitoring_system.personnel_access_record as p on ab.personnel_access_record_id = p.id
                                                  where ab.status = 'O'
                                                    and ab.next_waring_time = @nextWaringTime
                                                  """;

       /// <summary>
       ///     是否存在返回记录
       /// </summary>
       public const string IsExistReturnRecord = """
                                                 select p.id               as Id,
                                                 p.access_time      as AccessTime,
                                                 p.access_status    as AccessStatus,
                                                 p.event_identifier as EventIdentifier
                                                 from intelligent_monitoring_system.personnel_access_record as p
                                                 where p.event_identifier in @eventIdentifiers
                                                   and not exists(select 1
                                                                  from intelligent_monitoring_system.personnel_access_record as pr
                                                                  where pr.face_id = p.face_id
                                                                    and pr.access_type = 'E'
                                                                    and pr.id > p.id)
                                                 """;

       /// <summary>
       ///     保存未按时返回记录至异常记录表
       /// </summary>
       public const string SaveNotReturnOnTimeRecordSql = """
                                                          DROP TEMPORARY TABLE IF EXISTS tmp;

                                                          create temporary table tmp as (select p.id                                          as Id,
                                                                                                p.face_id                                     AS FaceId,
                                                                                                p.access_time                                 as AccessTime,
                                                                                                p.access_status                               as AccessStatus,
                                                                                                p.event_identifier                            as EventIdentifier,
                                                                                                ifnull(p.expected_return_time, p.access_time) as ExpectedReturnTime
                                                                                         from intelligent_monitoring_system.personnel_access_record as p
                                                                                         where p.event_identifier in @eventIdentifiers
                                                                                           and not exists(select 1
                                                                                                          from intelligent_monitoring_system.personnel_access_record as pr
                                                                                                          where pr.face_id = p.face_id
                                                                                                            and pr.access_type = 'E'
                                                                                                            and pr.id > p.id)
                                                                                           and not exists(select 1
                                                                                                          from intelligent_monitoring_system.abnormal_personnel_access_record as apr
                                                                                                          where apr.personnel_access_record_id = p.id));


                                                          update intelligent_monitoring_system.personnel_access_record
                                                          set access_status       = 'A',
                                                              last_edit_time      = now(),
                                                              last_edit_user_id   ='0',
                                                              last_edit_user_name = 'Job-PersonnelAccessRecordDelayEventInvocable'
                                                          where id in (select Id from tmp);

                                                          INSERT INTO intelligent_monitoring_system.abnormal_personnel_access_record(personnel_access_record_id, leave_time,
                                                                                                                                     status, face_id, create_time,
                                                                                                                                     next_waring_time)
                                                          select Id                 as personnel_access_record_id,
                                                                 ExpectedReturnTime as leave_time,
                                                                 'O'                as status,
                                                                 tmp.FaceId         as face_id,
                                                                 now()              as create_time,
                                                                 @nextWaringTime    as next_waring_time
                                                          from tmp;

                                                          DROP TEMPORARY TABLE IF EXISTS tmp;
                                                          """;

       /// <summary>
       ///     查询异常记录
       /// </summary>
       public const string PageSearchAbnormalPersonnelAccessRecordSql = """
                                                                        select ap.status                     as Status,
                                                                               ap.id                         as Id,
                                                                               ap.personnel_access_record_id as PersonnelAccessRecordId,
                                                                               ap.leave_time                 as LeaveTime,
                                                                               ap.return_time                as ReturnTime,
                                                                               par.name                      as Name,
                                                                               par.age                       as Age,
                                                                               par.gender                    as Gender,
                                                                               ua.room_no                    as RoomNo,
                                                                               ua.nurse                      as Nurse,
                                                                               par.leave_status              as LeaveStatus,
                                                                               par.leave_application_id      as LeaveApplicationId
                                                                        from intelligent_monitoring_system.abnormal_personnel_access_record as ap
                                                                                 inner join intelligent_monitoring_system.personnel_access_record par on ap.personnel_access_record_id = par.id
                                                                                 inner join intelligent_monitoring_system.t_user_account as ua on ua.face_id = ap.face_id
                                                                        where 1 = 1
                                                                        """;

       /// <summary>
       ///     查询异常记录
       /// </summary>
       public const string GetAbnormalPersonnelAccessRecordSql = """
                                                                 select ap.status                     as Status,
                                                                        ap.id                         as Id,
                                                                        ap.personnel_access_record_id as PersonnelAccessRecordId,
                                                                        ap.leave_time                 as LeaveTime,
                                                                        ap.return_time                as ReturnTime,
                                                                        par.name                      as Name,
                                                                        par.access_time               as AccessTime,
                                                                        par.age                       as Age,
                                                                        par.gender                    as Gender,
                                                                        par.device_id                 as DeviceId,
                                                                        par.device_name               as DeviceName,
                                                                        par.remark                    as Remark,
                                                                        par.original_face_url         as originalFaceUrl,
                                                                        par.original_snap_url         as originalSnapUrl,
                                                                        par.face_id                   as FaceId,
                                                                        par.face_img_path             as FaceImgPath,
                                                                        par.snap_img_path             as SnapImgPath,
                                                                        par.device_playback           as DevicePlayback
                                                                 from intelligent_monitoring_system.abnormal_personnel_access_record as ap
                                                                          inner join intelligent_monitoring_system.personnel_access_record par on ap.personnel_access_record_id = par.id
                                                                 where ap.id = @id
                                                                 limit 1;
                                                                 """;

       /// <summary>
       ///     查询异常记录数量
       /// </summary>
       public const string PageSearchAbnormalPersonnelAccessRecordCountSql = """
                                                                             select count(1)
                                                                             from intelligent_monitoring_system.abnormal_personnel_access_record as ap
                                                                                      inner join intelligent_monitoring_system.personnel_access_record par on ap.personnel_access_record_id = par.id
                                                                                      inner join intelligent_monitoring_system.t_user_account as ua on ua.face_id = ap.face_id
                                                                             where 1 = 1
                                                                             """;

       /// <summary>
       ///     出入记录统计
       /// </summary>
       public const string PersonnelAccessRecordStatisticsSql = """
                                                                SELECT COUNT(*)                           AS TotalCount,
                                                                       SUM(IF(access_type = 'E', 1, 0))   AS EnterCount,
                                                                       SUM(IF(access_type = 'L', 1, 0))   AS LeaveCount,
                                                                       SUM(IF(access_status = 'A', 1, 0)) AS AbnormalLeaveCount
                                                                FROM intelligent_monitoring_system.personnel_access_record
                                                                WHERE access_time between @beginTime and @endTime
                                                                limit 1;
                                                                """;

       /// <summary>
       ///     异常出入记录统计
       /// </summary>
       public const string AbnormalPersonnelAccessRecordStatisticsSql = """
                                                                        SELECT SUM(DATE_FORMAT(leave_time, '%Y%m') = DATE_FORMAT(@filterTime, '%Y%m')) MonthCount,
                                                                               SUM(QUARTER(leave_time) = QUARTER(@filterTime))                             QuarterCount,
                                                                               SUM(YEAR(leave_time) = YEAR(@filterTime))                                   YearCount,
                                                                               COUNT(1)                                                              TotalCount
                                                                        FROM intelligent_monitoring_system.abnormal_personnel_access_record
                                                                           limit 1;
                                                                        """;

       /// <summary>
       ///     统计排行版查询
       /// </summary>
       public const string StatisticsRankingSql = """
                                                  WITH TimeDifferences as (
                                                      SELECT
                                                          face_id,
                                                          COUNT(1) as count,
                                                          SUM(TIMESTAMPDIFF(SECOND, leave_time, IF(return_time IS NULL, NOW(), return_time))) AS totalTime
                                                      FROM
                                                          intelligent_monitoring_system.abnormal_personnel_access_record
                                                      where status = 'I'
                                                      GROUP BY
                                                          face_id
                                                  )
                                                  SELECT t.face_id                           as FaceId,
                                                         u.user_name                         as UserName,
                                                         count                               as Count,
                                                         totalTime                           as TotalTime,
                                                         cast(totalTime / count as unsigned) AS AverageTime
                                                  FROM TimeDifferences as t
                                                           inner join intelligent_monitoring_system.t_user_account as u on t.face_id = u.face_id
                                                  """;

       /// <summary>
       ///     用户异常出入记录统计
       /// </summary>
       public const string UserAbnormalPersonnelAccessRecordStatisticsSql = """
                                                                            SELECT face_id                                                                             as FaceId,
                                                                                   COUNT(1)                                                                            as Count,
                                                                                   SUM(TIMESTAMPDIFF(SECOND, leave_time, IF(return_time IS NULL, NOW(), return_time))) AS TotalTime
                                                                            FROM intelligent_monitoring_system.abnormal_personnel_access_record
                                                                            where status = 'I'
                                                                              and face_id = @faceId
                                                                              limit 1;
                                                                            """;

       /// <summary>
       ///     更新异常记录下次预警时间
       /// </summary>
       public const string UpdateAbnormalPersonnelAccessRecordNextWaringSql = """
                                                                              update intelligent_monitoring_system.abnormal_personnel_access_record
                                                                              set next_waring_time = @nextWaringTime
                                                                              where id in @ids
                                                                              """;
}