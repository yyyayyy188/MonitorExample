using System.Data;
using Microsoft.Extensions.Hosting;
using MySqlConnector;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;

namespace IntelligentMonitoringSystem.Infrastructure.Dapper.DbContexts;

/// <summary>
///     Connection factory
/// </summary>
/// <param name="environment">environment</param>
/// <param name="dataSource">dataSource</param>
/// <typeparam name="TDbContext">TDbContext</typeparam>
public class ConnectionFactory<TDbContext>(
    IHostEnvironment environment,
    MySqlDataSource dataSource)
    : IConnectionFactory<TDbContext>
    where TDbContext : IDapperDbContext
{
    /// <summary>
    ///     Get connection
    /// </summary>
    /// <returns>IDbConnection</returns>
    public IDbConnection GetConnection()
    {
        var connection = dataSource.CreateConnection();
        if (environment.IsProduction())
        {
            return connection;
        }

        return new ProfiledDbConnection(connection, MiniProfiler.Current);
    }
}