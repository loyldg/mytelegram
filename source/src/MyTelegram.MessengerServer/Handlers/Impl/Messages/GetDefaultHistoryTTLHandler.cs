// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

internal sealed class GetDefaultHistoryTTLHandler :
    RpcResultObjectHandler<RequestGetDefaultHistoryTTL, Schema.IDefaultHistoryTTL>,
    Messages.IGetDefaultHistoryTTLHandler
{
    protected override Task<Schema.IDefaultHistoryTTL> HandleCoreAsync(IRequestInput input,
        RequestGetDefaultHistoryTTL obj)
    {
        throw new NotImplementedException();
    }
}
