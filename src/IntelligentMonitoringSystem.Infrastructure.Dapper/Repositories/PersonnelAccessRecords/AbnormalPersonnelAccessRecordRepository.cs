using System.Data;
using System.Text;
using Dapper;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Const;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using IntelligentMonitoringSystem.Infrastructure.Dapper.DbContexts;
using IntelligentMonitoringSystem.Infrastructure.Dapper.Repositories.PersonnelAccessRecords.Po;
using static IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Const.PersonnelAccessRecordSqlConst;

namespace IntelligentMonitoringSystem.Infrastructure.Dapper.Repositories.PersonnelAccessRecords;

/// <summary>
///     预警记录
/// </summary>
/// <param name="connectionFactory">connectionFactory</param>
public class AbnormalPersonnelAccessRecordRepository(IConnectionFactory<DapperDbContext> connectionFactory)
    : DapperReadOnlyBasicRepository<DapperDbContext, AbnormalPersonnelAccessRecord>(connectionFactory),
        IAbnormalPersonnelAccessRecordRepository
{
    private readonly IConnectionFactory<DapperDbContext> _connectionFactory = connectionFactory;

    /// <summary>
    ///     查询告警记录
    /// </summary>
    /// <param name="waringTime">告警时间</param>
    /// <returns>异常出入集合</returns>
    public async Task<List<AbnormalPersonnelAccessRecord>> QueryWaringRecordAsync(DateTime waringTime)
    {
        using var dbConnection = _connectionFactory.GetConnection();
        var records = await dbConnection.QueryAsync<AbnormalPersonnelAccessRecordPo>(
            QueryWaringRecordSql, new { nextWaringTime = waringTime });
        var abnormalPersonnelAccessRecordPos = records as AbnormalPersonnelAccessRecordPo[] ?? records.ToArray();
        if (abnormalPersonnelAccessRecordPos.Length == 0)
        {
            return [];
        }

        return abnormalPersonnelAccessRecordPos.Select(record => new AbnormalPersonnelAccessRecord
        {
            Id = record.Id,
            PersonnelAccessRecordId = record.PersonnelAccessRecordId,
            PersonnelAccessRecord = new PersonnelAccessRecord { Id = record.PersonnelAccessRecordId }
        }).ToList();
    }

    /// <summary>
    ///     获取异常出入记录
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>AbnormalPersonnelAccessRecord</returns>
    public async Task<AbnormalPersonnelAccessRecord?> GetAbnormalPersonnelAccessRecordAsync(int id)
    {
        using var dbConnection = _connectionFactory.GetConnection();
        var record = await dbConnection.QueryFirstOrDefaultAsync<AbnormalPersonnelAccessRecordPo>(
            GetAbnormalPersonnelAccessRecordSql, new { id });

        if (record == null)
        {
            return null;
        }

        return new AbnormalPersonnelAccessRecord
        {
            Id = record.Id,
            PersonnelAccessRecordId = record.PersonnelAccessRecordId,
            LeaveTime = record.LeaveTime,
            ReturnTime = record.ReturnTime,
            FaceId = record.FaceId,
            Status = record.Status,
            PersonnelAccessRecord = new PersonnelAccessRecord
            {
                Id = record.PersonnelAccessRecordId,
                Name = record.Name,
                Age = record.Age,
                Gender = record.Gender,
                DeviceId = record.DeviceId,
                DeviceName = record.DeviceName,
                AccessTime = record.AccessTime,
                OriginalFaceUrl = record.OriginalFaceUrl,
                OriginalSnapUrl = record.OriginalSnapUrl,
                FaceImgPath = record.FaceImg,
                FaceId = record.FaceId,
                Remark = record.Remark,
                DevicePlayback = record.DevicePlayback,
                SnapImgPath = record.SnapImg
            }
        };
    }

    /// <summary>
    ///     分页查询
    /// </summary>
    /// <param name="pageSize">页面大小</param>
    /// <param name="skipCount">跳过行数</param>
    /// <param name="name">姓名</param>
    /// <param name="status">状态</param>
    /// <param name="leaveStartTime">离开的开始时间</param>
    /// <param name="leaveEndTime">离开的结束时间</param>
    /// <param name="enterStartTime">进入的开始时间</param>
    /// <param name="enterEndTime">进入的结束时间</param>
    /// <returns>List<AbnormalPersonnelAccessRecord /></returns>
    public async Task<List<AbnormalPersonnelAccessRecord>> PageSearchAsync(int pageSize, int skipCount, string name,
        AbnormalPersonnelAccessRecordStatusSmartEnum? status, DateTime? leaveStartTime, DateTime?
            leaveEndTime, DateTime? enterStartTime, DateTime? enterEndTime)
    {
        var dynamicParams = new DynamicParameters();
        dynamicParams.Add("pageSize", pageSize);
        dynamicParams.Add("skipCount", skipCount);
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(PageSearchAbnormalPersonnelAccessRecordSql);
        BuildSearchDynamicSql(stringBuilder, dynamicParams, name, status, leaveStartTime, leaveEndTime, enterStartTime,
            enterEndTime, null);
        stringBuilder.Append(" order by ap.return_time is null desc ,ap.return_time desc , ap.leave_time desc ");
        stringBuilder.Append(" limit @pageSize offset @skipCount");
        using var dbConnection = _connectionFactory.GetConnection();
        var records = await dbConnection.QueryAsync<AbnormalPersonnelAccessRecordPo>(
            stringBuilder.ToString(), dynamicParams);
        var abnormalPersonnelAccessRecordPos = records as AbnormalPersonnelAccessRecordPo[] ?? records.ToArray();
        if (abnormalPersonnelAccessRecordPos.Length == 0)
        {
            return [];
        }

        return abnormalPersonnelAccessRecordPos.Select(record => new AbnormalPersonnelAccessRecord
        {
            Id = record.Id,
            PersonnelAccessRecordId = record.PersonnelAccessRecordId,
            LeaveTime = record.LeaveTime,
            ReturnTime = record.ReturnTime,
            Status = record.Status,
            PersonnelAccessRecord = new PersonnelAccessRecord
            {
                Id = record.PersonnelAccessRecordId,
                Name = record.Name,
                Age = record.Age,
                Gender = record.Gender,
                Nurse = record.Nurse,
                RoomNo = record.RoomNo,
                LeaveApplicationId = record.LeaveApplicationId,
                LeaveStatus = record.LeaveStatus
            }
        }).ToList();
    }

    /// <summary>
    ///     查询
    /// </summary>
    /// <param name="name">姓名</param>
    /// <param name="status">状态</param>
    /// <param name="leaveStartTime">离开的开始时间</param>
    /// <param name="leaveEndTime">离开的结束时间</param>
    /// <param name="enterStartTime">进入的开始时间</param>
    /// <param name="enterEndTime">进入的结束时间</param>
    /// <param name="ids">查询指定Id集合</param>
    /// <returns>IEnumerable<AbnormalPersonnelAccessRecord /></returns>
    public async Task<IEnumerable<AbnormalPersonnelAccessRecord>> SearchAsync(
        string name, AbnormalPersonnelAccessRecordStatusSmartEnum status, DateTime? leaveStartTime,
        DateTime? leaveEndTime, DateTime? enterStartTime, DateTime? enterEndTime, List<int> ids)
    {
        var dynamicParams = new DynamicParameters();
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(PageSearchAbnormalPersonnelAccessRecordSql);
        BuildSearchDynamicSql(stringBuilder, dynamicParams, name, status, leaveStartTime, leaveEndTime, enterStartTime,
            enterEndTime, ids);
        using var dbConnection = _connectionFactory.GetConnection();
        var records = await dbConnection.QueryAsync<AbnormalPersonnelAccessRecordPo>(
            stringBuilder.ToString(), dynamicParams);
        var abnormalPersonnelAccessRecordPos = records as AbnormalPersonnelAccessRecordPo[] ?? records.ToArray();
        if (abnormalPersonnelAccessRecordPos.Length == 0)
        {
            return [];
        }

        return abnormalPersonnelAccessRecordPos.Select(record => new AbnormalPersonnelAccessRecord
        {
            Id = record.Id,
            PersonnelAccessRecordId = record.PersonnelAccessRecordId,
            LeaveTime = record.LeaveTime,
            ReturnTime = record.ReturnTime,
            Status = record.Status,
            PersonnelAccessRecord = new PersonnelAccessRecord
            {
                Id = record.PersonnelAccessRecordId,
                Name = record.Name,
                Age = record.Age,
                Gender = record.Gender,
                Nurse = record.Nurse,
                RoomNo = record.RoomNo
            }
        }).ToList();
    }

    /// <summary>
    ///     分页数量统计
    /// </summary>
    /// <param name="name">姓名</param>
    /// <param name="status">状态</param>
    /// <param name="leaveStartTime">离开的开始时间</param>
    /// <param name="leaveEndTime">离开的结束时间</param>
    /// <param name="enterStartTime">进入的开始时间</param>
    /// <param name="enterEndTime">进入的结束时间</param>
    /// <returns>List<AbnormalPersonnelAccessRecord /></returns>
    public async Task<int> SearchTotalCountAsync(string name, AbnormalPersonnelAccessRecordStatusSmartEnum status,
        DateTime? leaveStartTime,
        DateTime? leaveEndTime,
        DateTime? enterStartTime, DateTime? enterEndTime)
    {
        var dynamicParams = new DynamicParameters();
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(PageSearchAbnormalPersonnelAccessRecordCountSql);
        BuildSearchDynamicSql(stringBuilder, dynamicParams, name, status, leaveStartTime, leaveEndTime, enterStartTime,
            enterEndTime, null);
        using var dbConnection = _connectionFactory.GetConnection();
        var totalCount = await dbConnection.ExecuteScalarAsync<int>(stringBuilder.ToString(), dynamicParams);
        return totalCount;
    }

    /// <summary>
    ///     保存未按时返回的数据值异常记录表并更新原始状态
    /// </summary>
    /// <param name="eventIdentifiers">事件Id</param>
    /// <param name="nextWaringTime">下次告警时间</param>
    /// <returns>Task</returns>
    public async Task SaveNotReturnOnTimeRecordAsync(List<Guid> eventIdentifiers, DateTime nextWaringTime)
    {
        using var dbConnection = _connectionFactory.GetConnection();
        await dbConnection.ExecuteAsync(
            SaveNotReturnOnTimeRecordSql, new { eventIdentifiers, nextWaringTime });
    }

    /// <summary>
    ///     获取异常外出平均时间
    /// </summary>
    /// <param name="faceId">面容Id</param>
    /// <returns>平均时长</returns>
    public async Task<long> GetAbnormalAccessAverageTimeAsync(string faceId)
    {
        using var dbConnection = _connectionFactory.GetConnection();
        var abnormalAccessStatistics = await dbConnection.QueryFirstOrDefaultAsync<AbnormalAccessStatisticsPo>(
            UserAbnormalPersonnelAccessRecordStatisticsSql, new { faceId });
        if (abnormalAccessStatistics == null || string.IsNullOrWhiteSpace(abnormalAccessStatistics.FaceId) ||
            abnormalAccessStatistics.Count == 0)
        {
            return 0;
        }

        return abnormalAccessStatistics.TotalTime / abnormalAccessStatistics.Count;
    }

    /// <summary>
    ///     更新异常外出记录下次告警时间
    /// </summary>
    /// <param name="ids">ids</param>
    /// <param name="nextWaringTime">下次告警时间</param>
    /// <returns>Task</returns>
    public async Task UpdateAbnormalPersonnelAccessRecordNextWaring(List<int>? ids, DateTime nextWaringTime)
    {
        if (ids == null || ids.Count == 0)
        {
            return;
        }

        using var dbConnection = _connectionFactory.GetConnection();
        await dbConnection.ExecuteAsync(UpdateAbnormalPersonnelAccessRecordNextWaringSql, new { nextWaringTime, ids });
    }

    /// <summary>
    ///     构建动态sql
    /// </summary>
    /// <param name="stringBuilder">基础Sql</param>
    /// <param name="dynamicParams">参数化</param>
    /// <param name="name">姓名</param>
    /// <param name="status">状态</param>
    /// <param name="leaveStartTime">离开的开始时间</param>
    /// <param name="leaveEndTime">离开的结束时间</param>
    /// <param name="enterStartTime">进入的开始时间</param>
    /// <param name="enterEndTime">进入的结束时间</param>
    /// <param name="ids">ids</param>
    private void BuildSearchDynamicSql(StringBuilder stringBuilder, DynamicParameters dynamicParams, string name,
        AbnormalPersonnelAccessRecordStatusSmartEnum? status, DateTime? leaveStartTime, DateTime? leaveEndTime,
        DateTime? enterStartTime, DateTime? enterEndTime, List<int>? ids)
    {
        if (ids != null)
        {
            if (ids.Count == 0)
            {
                return;
            }

            stringBuilder.Append(" and ap.id in @ids ");
            dynamicParams.Add("ids", ids);
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                stringBuilder.Append(" and par.name like @name ");
                dynamicParams.Add("name", $"%{name}%", DbType.AnsiString, ParameterDirection.Input, 64);
            }

            if (status is not null)
            {
                stringBuilder.Append(" and ap.status = @status ");
                dynamicParams.Add("status", status.Value, DbType.AnsiStringFixedLength, ParameterDirection.Input, 1);
            }

            if (leaveStartTime is not null && leaveEndTime is not null)
            {
                stringBuilder.Append(" and ap.leave_time >= @leaveStartTime  and ap.leave_time <= @leaveEndTime ");
                dynamicParams.Add(
                    "leaveStartTime", leaveStartTime.Value.ToString(PersonnelAccessRecordConst.FormatFilterDateTime));
                dynamicParams.Add("leaveEndTime", leaveEndTime.Value.ToString(PersonnelAccessRecordConst.FormatFilterDateTime));
            }

            if (enterStartTime is null || enterEndTime is null)
            {
                return;
            }

            stringBuilder.Append(" and ap.return_time >= @enterStartTime and ap.return_time <= @enterEndTime ");
            dynamicParams.Add("enterStartTime", enterStartTime.Value.ToString(PersonnelAccessRecordConst.FormatFilterDateTime));
            dynamicParams.Add("enterEndTime", enterEndTime.Value.ToString(PersonnelAccessRecordConst.FormatFilterDateTime));
        }
    }
}