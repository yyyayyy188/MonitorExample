namespace IntelligentMonitoringSystem.Infrastructure.Reference.Apis.DownLoads;

/// <summary>
///     动态路由设置
/// </summary>
public class DynamicUrlDelegatingHandler : DelegatingHandler
{
    /// <summary>
    ///     发送请求
    /// </summary>
    /// <param name="request">request.</param>
    /// <param name="cancellationToken">cancellationToken.</param>
    /// <returns>HttpResponseMessage.</returns>
    protected async override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (!request.Options.TryGetValue(new HttpRequestOptionsKey<string>("url"), out var url) ||
            string.IsNullOrWhiteSpace(url))
        {
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        var uri = new Uri(url);
        request.RequestUri = uri;

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}