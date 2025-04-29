// <copyright file="VideoAuthenticatedHttpClientHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Web;
using IntelligentMonitoringSystem.Domain.Shared.Configs;
using Microsoft.Extensions.Options;

namespace IntelligentMonitoringSystem.Infrastructure.Reference.Apis.Videos.DelegatingHandlers;

/// <summary>
///     视频接口授权认证处理器.
/// </summary>
/// <param name="options">options.</param>
public class VideoAuthenticatedHttpClientHandler(IOptionsMonitor<IntelligentMonitorSystemHttpClientConfig> options)
    : DelegatingHandler
{
    /// <summary>
    ///     添加授权认证.
    /// </summary>
    /// <param name="request">request.</param>
    /// <param name="cancellationToken">cancellationToken.</param>
    /// <returns>Task{HttpResponseMessage}.</returns>
    protected async override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (!request.Options.TryGetValue(new HttpRequestOptionsKey<string>("apiName"), out var apiName) ||
            string.IsNullOrWhiteSpace(apiName))
        {
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        var apiEntity = options.CurrentValue.GetApiEntity("OpenVideo", apiName);
        if (apiEntity.QueryParams.Count == 0 || request.RequestUri == null)
        {
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        var queryParams = apiEntity.QueryParams;
        var builder = new UriBuilder(request.RequestUri);
        var httpValueCollection = HttpUtility.ParseQueryString(request.RequestUri.Query);
        foreach (var query in queryParams)
        {
            httpValueCollection.Add(query.Key, query.Value);
        }

        builder.Query = httpValueCollection.ToString();
        request.RequestUri = builder.Uri;

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}