using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class UnblockHandler : RpcResultObjectHandler<RequestUnblock, IBool>,
    IUnblockHandler, IProcessedHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestUnblock obj)
    {
        throw new NotImplementedException();
    }
}