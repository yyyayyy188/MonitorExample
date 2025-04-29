using System.Data;
using Dapper;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using IntelligentMonitoringSystem.Domain.Shared.Users.Const;
using IntelligentMonitoringSystem.Domain.Users;
using IntelligentMonitoringSystem.Infrastructure.Dapper.DbContexts;

namespace IntelligentMonitoringSystem.Infrastructure.Dapper.Repositories.Users;

/// <summary>
///     用户仓储
/// </summary>
public class UserRepository(IConnectionFactory<DapperDbContext> connectionFactory) : IUserRepository
{
    /// <summary>
    ///     同步异常访问次数
    /// </summary>
    /// <param name="faceId">faceId</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task SynchronizationAbnormalLeaveAccessInfoAsync(string faceId)
    {
        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("@faceId", faceId, DbType.AnsiString, ParameterDirection.Input, 64);
        using var dbConnection = connectionFactory.GetConnection();
        dynamicParameters.Add("@roomStatus", 0, DbType.Int32, ParameterDirection.Input);
        await dbConnection.ExecuteAsync(
            UserConstSql.SynchronizationAbnormalLeaveAccessInfoSql, dynamicParameters);
    }

    /// <summary>
    ///     同步用户信息
    /// </summary>
    /// <param name="faceId">面容Id</param>
    /// <param name="accessTime">访问时间</param>
    /// <param name="accessType">访问类型</param>
    /// <returns>Task</returns>
    public async Task SynchronizationAccessInfoAsync(string faceId, DateTime accessTime, AccessTypeSmartEnum accessType)
    {
        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("@accessTime", accessTime, DbType.DateTime, ParameterDirection.Input);
        dynamicParameters.Add("@faceId", faceId, DbType.AnsiString, ParameterDirection.Input, 64);
        using var dbConnection = connectionFactory.GetConnection();
        if (accessType == AccessTypeSmartEnum.Enter)
        {
            dynamicParameters.Add("@roomStatus", 0, DbType.Int32, ParameterDirection.Input);
            await dbConnection.ExecuteAsync(
                UserConstSql.SynchronizationEnterAccessInfoSql, dynamicParameters);
        }
        else
        {
            dynamicParameters.Add("@roomStatus", 1, DbType.Int32, ParameterDirection.Input);
            await dbConnection.ExecuteAsync(
                UserConstSql.SynchronizationLeaveAccessInfoSql, dynamicParameters);
        }
    }

    /// <summary>
    ///     同步异常访问平均时长
    /// </summary>
    /// <param name="faceId">面容Id</param>
    /// <param name="averageTime">平均时长</param>
    /// <returns>Task</returns>
    public async Task SynchronizationAbnormalAccessAverageTimeAsync(string faceId, long averageTime)
    {
        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("@averageTime", averageTime, DbType.Double, ParameterDirection.Input);
        dynamicParameters.Add("@faceId", faceId, DbType.AnsiString, ParameterDirection.Input, 64);
        using var dbConnection = connectionFactory.GetConnection();
        await dbConnection.ExecuteAsync(UserConstSql.SynchronizationAbnormalAccessAverageTimeSql, dynamicParameters);
    }

    /// <summary>
    ///     获取用户账户信息
    /// </summary>
    /// <param name="faceId">面容Id</param>
    /// <returns>UserAccount</returns>
    public async Task<UserAccount?> GetUserAccountAsync(string faceId)
    {
        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("@faceId", faceId, DbType.AnsiString, ParameterDirection.Input, 64);
        using var dbConnection = connectionFactory.GetConnection();
        return await dbConnection.QueryFirstOrDefaultAsync<UserAccount>(
            UserConstSql.GetUserAccountSql, dynamicParameters);
    }
}