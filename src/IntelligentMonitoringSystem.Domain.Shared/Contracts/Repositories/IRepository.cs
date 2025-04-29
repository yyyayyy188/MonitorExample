namespace IntelligentMonitoringSystem.Domain.Shared.Contracts.Repositories;

/// <summary>
///     仓储接口
/// </summary>
public interface IRepository
{
    /// <summary>
    ///     初始化上下文
    /// </summary>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task InitDbContextAsync();
}