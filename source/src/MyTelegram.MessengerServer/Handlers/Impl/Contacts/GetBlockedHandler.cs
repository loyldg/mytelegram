using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class GetBlockedHandler : RpcResultObjectHandler<RequestGetBlocked, IBlocked>,
    IGetBlockedHandler
{
    protected override Task<IBlocked> HandleCoreAsync(IRequestInput input,
        RequestGetBlocked obj)
    {
        throw new NotImplementedException();
    }
}