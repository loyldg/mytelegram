// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

internal sealed class SetDefaultHistoryTTLHandler : RpcResultObjectHandler<RequestSetDefaultHistoryTTL, IBool>,
    Messages.ISetDefaultHistoryTTLHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetDefaultHistoryTTL obj)
    {
        throw new NotImplementedException();
    }
}
