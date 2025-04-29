using System.Data;

namespace IntelligentMonitoringSystem.Infrastructure.Dapper.DbContexts;

/// <summary>
///     数据库连接工厂
/// </summary>
public interface IConnectionFactory<TDbContext> where TDbContext : IDapperDbContext
{
    /// <summary>
    ///     获取数据库连接
    /// </summary>
    /// <returns>IDbConnection</returns>
    IDbConnection GetConnection();
}