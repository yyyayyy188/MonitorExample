using Microsoft.AspNetCore.Mvc;

namespace IntelligentMonitoringSystem.WebApi.Extensions;

/// <summary>
///     Attribute to add route to external service.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class CleanSvcRouteAttribute(string template = "/") : RouteAttribute(ExternalRouteBase + template)
{
    /// <summary>
    ///     基础路由
    /// </summary>
    public const string ExternalRouteBase = "/api/monitoring-svc";

    /// <summary>
    ///     Swagger 路由
    /// </summary>
    public const string SwaggerRouteBase = "api/monitoring-svc";
}