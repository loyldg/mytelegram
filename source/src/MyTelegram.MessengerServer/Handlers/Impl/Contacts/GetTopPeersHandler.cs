using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class GetTopPeersHandler : RpcResultObjectHandler<RequestGetTopPeers, ITopPeers>,
    IGetTopPeersHandler, IProcessedHandler
{
    protected override Task<ITopPeers> HandleCoreAsync(IRequestInput input,
        RequestGetTopPeers obj)
    {
        ITopPeers r = new TTopPeers
        {
            Categories = new TVector<ITopPeerCategoryPeers>(),
            Chats = new TVector<IChat>(),
            Users = new TVector<IUser>()
        };

        return Task.FromResult(r);
    }
}
