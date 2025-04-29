using IntelligentMonitoringSystem.Application.Common.Contracts;
using IntelligentMonitoringSystem.Application.MessageCenters.MessageDataResolvers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.MessageCenters.WebSocket;

/// <summary>
///     WebSocket消息查询处理器
/// </summary>
public class WebSocketMessageQueryHandler(IServiceProvider serviceProvider)
    : IQueryHandler<WebSocketMessageQuery, string>
{
    /// <summary>
    ///     处理
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>string</returns>
    public async Task<string> Handle(WebSocketMessageQuery request, CancellationToken cancellationToken)
    {
        var messageDataResolver =
            serviceProvider.GetRequiredKeyedService<IMessageDataResolver>(request.EventSource
                .MessageDataResolverProvider);
        var data = await messageDataResolver.DataResolver(request.EventSource, request.EventSourceData);
        return JsonConvert.SerializeObject(data);
    }
}