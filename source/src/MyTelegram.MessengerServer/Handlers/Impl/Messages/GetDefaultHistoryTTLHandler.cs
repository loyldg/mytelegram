// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

internal sealed class GetDefaultHistoryTTLHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetDefaultHistoryTTL, MyTelegram.Schema.IDefaultHistoryTTL>,
    Messages.IGetDefaultHistoryTTLHandler
{
    protected override Task<MyTelegram.Schema.IDefaultHistoryTTL> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetDefaultHistoryTTL obj)
    {
        throw new NotImplementedException();
    }
}
