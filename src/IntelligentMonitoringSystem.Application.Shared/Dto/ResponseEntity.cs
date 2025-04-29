using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.Shared.Dto;

/// <summary>
///     ResponseEntity
/// </summary>
public class ResponseEntity
{
    /// <summary>
    ///     isSuccess
    /// </summary>
    [JsonProperty("success")]
    public bool Success { get; set; }

    /// <summary>
    ///     Code
    /// </summary>
    [JsonProperty("code")]
    public int Code { get; set; }

    /// <summary>
    ///     Message
    /// </summary>
    [JsonProperty("message")]
    public string? Message { get; set; }

    /// <summary>
    ///     Ok
    /// </summary>
    /// <param name="isSuccessful">isSuccessful</param>
    /// <returns>ResponseEntity</returns>
    public static ResponseEntity Ok(bool isSuccessful = true)
    {
        return new ResponseEntity { Message = "success", Success = isSuccessful };
    }
}

/// <summary>
///     ResponseEntity
/// </summary>
/// <typeparam name="TResult"></typeparam>
public class ResponseEntity<TResult> : ResponseEntity
{
    /// <summary>
    ///     Data
    /// </summary>
    [JsonProperty("data")]
    public TResult? Data { get; set; }

    /// <summary>
    ///     Ok
    /// </summary>
    /// <param name="data">data</param>
    /// <param name="isSuccessful">isSuccessful</param>
    /// <param name="message">消息</param>
    /// <param name="code">业务代码</param>
    /// <returns>ResponseEntity</returns>
    public static ResponseEntity<TResult> Ok(TResult data, bool isSuccessful = true, string message = "success", int code = 200)
    {
        return new ResponseEntity<TResult> { Data = data, Success = isSuccessful, Message = message, Code = code };
    }
}