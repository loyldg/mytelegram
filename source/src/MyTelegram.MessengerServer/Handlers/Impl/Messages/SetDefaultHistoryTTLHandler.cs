// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

internal sealed class SetDefaultHistoryTTLHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetDefaultHistoryTTL, IBool>,
    Messages.ISetDefaultHistoryTTLHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSetDefaultHistoryTTL obj)
    {
        throw new NotImplementedException();
    }
}
