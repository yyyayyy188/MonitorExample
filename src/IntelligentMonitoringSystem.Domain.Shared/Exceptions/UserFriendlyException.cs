using Microsoft.Extensions.Logging;

namespace IntelligentMonitoringSystem.Domain.Shared.Exceptions;

public class UserFriendlyException(
    string message,
    string? code = null,
    LogLevel logLevel = LogLevel.Warning)
    : BusinessException(code,
        message,
        logLevel);