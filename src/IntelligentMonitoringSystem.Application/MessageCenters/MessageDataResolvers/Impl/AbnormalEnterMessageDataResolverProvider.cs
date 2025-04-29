using System.Threading.Tasks;

namespace IntelligentMonitoringSystem.Application.MessageCenters.MessageDataResolvers.Impl;

/// <summary>
/// 异常进入
/// </summary>
public class AbnormalEnterMessageDataResolverProvider : IMessageDataResolver
{
    public Task<object> DataResolver(object data)
    {
        throw new System.NotImplementedException();
    }
}