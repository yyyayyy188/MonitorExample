using IntelligentMonitoringSystem.Infrastructure.Common.Data;
using IntelligentMonitoringSystem.Infrastructure.Dapper.DbContexts;
using IntelligentMonitoringSystem.Infrastructure.Dapper.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace IntelligentMonitoringSystem.Infrastructure.Dapper;

/// <summary>
///     DapperDbContextExtensions
/// </summary>
public static class DapperDbContextExtensions
{
    /// <summary>
    ///     AddDapperDbContextAndDefaultRepository
    /// </summary>
    /// <param name="services">services</param>
    /// <param name="configuration">configuration</param>
    /// <typeparam name="TDbContext"></typeparam>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddDapperDbContextAndDefaultRepository<TDbContext>(
        this IServiceCollection services, IConfiguration configuration)
        where TDbContext : IDapperDbContext
    {
        var connectionStringName = ConnectionStringNameAttribute.GetConnStringName<TDbContext>();
        var connectionString = configuration.GetConnectionString(connectionStringName);
        services.AddSingleton(typeof(MySqlDataSource), serviceProvider =>
        {
            var dataSourceBuilder = new MySqlDataSourceBuilder(connectionString);
            dataSourceBuilder.UseLoggerFactory(serviceProvider.GetService<ILoggerFactory>());
            return dataSourceBuilder.Build();
        });

        services.AddSingleton<IConnectionFactory<TDbContext>, ConnectionFactory<TDbContext>>();
        var dapperRepositoryRegistrar = new DapperRepositoryRegistrar();
        dapperRepositoryRegistrar.RegisterDefaultRepositories<TDbContext>(services);
        return services;
    }
}