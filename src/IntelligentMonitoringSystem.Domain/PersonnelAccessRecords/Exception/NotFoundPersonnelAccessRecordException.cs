using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using Microsoft.Extensions.Logging;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Exception;

/// <summary>
///     未找到人员出入记录异常
/// </summary>
/// <param name="message">message</param>
/// <param name="code">code</param>
/// <param name="logLevel">logLevel</param>
public class NotFoundPersonnelAccessRecordException(
    string message = "为找到记录信息",
    string code = "400",
    LogLevel logLevel = LogLevel.Warning)
    : UserFriendlyException(message, code, logLevel);