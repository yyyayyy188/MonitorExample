using IntelligentMonitoringSystem.Domain.Shared.Contracts.UnitOfWorks;
using IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore.Repositories;
using IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore;

/// <summary>
///     EntityFrameWorkCoreDbContextExtensions
/// </summary>
public static class EntityFrameWorkCoreDbContextExtensions
{
    /// <summary>
    ///     AddDbContextAndDefaultRepository
    /// </summary>
    /// <param name="services">services</param>
    /// <param name="optionsAction">optionsAction</param>
    /// <param name="poolSize">连接池数量</param>
    /// <typeparam name="TDbContext"></typeparam>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddDbContextAndDefaultRepository<TDbContext>(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> optionsAction,
        int poolSize = 1024)
        where TDbContext : BaseDbContext
    {
        services.AddPooledDbContextFactory<TDbContext>(optionsAction, poolSize);
        services.AddScoped<BaseDbContextFactory<TDbContext>>();
        services.AddScoped<IUnitOfWorkAsync, EntityFrameWorkAsyncCoreUnitOfWorkAsync<TDbContext>>();
        services.AddTransient<IDbContextProvider<TDbContext>, DbContextProvider<TDbContext>>();
        var entityFrameWorkCoreRepositoryRegistrar = new EntityFrameWorkCoreRepositoryRegistrar();
        entityFrameWorkCoreRepositoryRegistrar.RegisterDefaultRepositories<TDbContext>(services);
        return services;
    }
}