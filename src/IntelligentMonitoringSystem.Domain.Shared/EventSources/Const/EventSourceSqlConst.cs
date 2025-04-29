namespace IntelligentMonitoringSystem.Domain.Shared.EventSources.Const;

/// <summary>
///     EventSource 存储相关SQL
/// </summary>
public static class EventSourceSqlConst
{
    /// <summary>
    ///     查询待处理的事件通过类型
    /// </summary>
    public const string QueryPendingRecordByTypeSql = """
                                                      DROP TEMPORARY TABLE IF EXISTS temp_updated_ids;
                                                      -- 创建临时表
                                                      CREATE TEMPORARY TABLE temp_updated_ids
                                                      (
                                                          id INT PRIMARY KEY
                                                      );

                                                      -- 选择需要更新的行并插入到临时表
                                                      INSERT INTO temp_updated_ids (id)
                                                      SELECT id
                                                      FROM intelligent_monitoring_system.event_stream
                                                      where status in (0,3)
                                                        and process_count <= @processCount
                                                        and type = @type
                                                      limit @count;

                                                      -- 更新原表
                                                      UPDATE intelligent_monitoring_system.event_stream
                                                      SET status = 1
                                                      WHERE id IN (SELECT id
                                                                   FROM temp_updated_ids);

                                                      -- 从临时表中选择更新后的数据

                                                      select id              as Id,
                                                             type            as Type,
                                                             full_type_name  as FullTypeName,
                                                             stream_id       as StreamId,
                                                             stream_position as StreamPosition,
                                                             create_time     as CreateTime,
                                                             status          as Status,
                                                             process_count   as ProcessCount,
                                                             data            as Data
                                                      from intelligent_monitoring_system.event_stream
                                                      where id in (SELECT id
                                                                   FROM temp_updated_ids);

                                                      -- 删除临时表
                                                      DROP TEMPORARY TABLE IF EXISTS temp_updated_ids;
                                                      """;

    /// <summary>
    ///     查询待处理的事件
    /// </summary>
    public const string QueryPendingRecordSql = """
                                                DROP TEMPORARY TABLE IF EXISTS temp_updated_ids;
                                                -- 创建临时表
                                                CREATE TEMPORARY TABLE temp_updated_ids
                                                (
                                                    id INT PRIMARY KEY
                                                );

                                                -- 选择需要更新的行并插入到临时表
                                                INSERT INTO temp_updated_ids (id)
                                                SELECT id
                                                FROM intelligent_monitoring_system.event_stream
                                                where status in (0,3)
                                                  and process_count <= @processCount
                                                  and type <> 'SynchronizationVideoPlaybackEvent'
                                                limit @count;

                                                -- 更新原表
                                                UPDATE intelligent_monitoring_system.event_stream
                                                SET status = 1
                                                WHERE id IN (SELECT id
                                                             FROM temp_updated_ids);

                                                -- 从临时表中选择更新后的数据

                                                select id              as Id,
                                                       type            as Type,
                                                       full_type_name  as FullTypeName,
                                                       stream_id       as StreamId,
                                                       stream_position as StreamPosition,
                                                       create_time     as CreateTime,
                                                       status          as Status,
                                                       process_count   as ProcessCount,
                                                       data            as Data
                                                from intelligent_monitoring_system.event_stream
                                                where id in (SELECT id
                                                             FROM temp_updated_ids);

                                                -- 删除临时表
                                                DROP TEMPORARY TABLE IF EXISTS temp_updated_ids;
                                                """;
}