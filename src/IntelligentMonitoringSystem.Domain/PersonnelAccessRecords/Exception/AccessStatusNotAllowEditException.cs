using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using Microsoft.Extensions.Logging;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Exception;

/// <summary>
///     出入状态不允许被修改
/// </summary>
/// <param name="message">message</param>
/// <param name="code">code</param>
/// <param name="logLevel">logLevel</param>
public class AccessStatusNotAllowEditException(
    string message = "当前出入状态不允许被修改",
    string code = "400",
    LogLevel logLevel = LogLevel.Warning)
    : UserFriendlyException(message, code, logLevel)
{
}