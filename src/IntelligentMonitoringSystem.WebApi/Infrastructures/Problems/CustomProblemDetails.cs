#nullable enable
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.WebApi.Infrastructures.Problems;

/// <summary>
///     CustomProblemDetails
/// </summary>
public class CustomProblemDetails : ProblemDetails
{
    /// <summary>
    ///     isSuccess
    /// </summary>
    [JsonProperty("success")]
    public bool Success => false;

    /// <summary>
    ///     Code
    /// </summary>
    [JsonProperty("code")]
    public string? Code { get; set; }

    /// <summary>
    ///     Message
    /// </summary>
    [JsonProperty("message")]
    public string? Message { get; set; }
}