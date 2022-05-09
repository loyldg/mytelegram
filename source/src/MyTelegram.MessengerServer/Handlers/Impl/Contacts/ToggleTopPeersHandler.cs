using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class ToggleTopPeersHandler : RpcResultObjectHandler<RequestToggleTopPeers, IBool>,
    IToggleTopPeersHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestToggleTopPeers obj)
    {
        throw new NotImplementedException();
    }
}
