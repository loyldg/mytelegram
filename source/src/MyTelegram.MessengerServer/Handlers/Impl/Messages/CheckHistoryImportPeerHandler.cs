using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class CheckHistoryImportPeerHandler :
    RpcResultObjectHandler<RequestCheckHistoryImportPeer, ICheckedHistoryImportPeer>,
    ICheckHistoryImportPeerHandler
{
    protected override Task<ICheckedHistoryImportPeer> HandleCoreAsync(IRequestInput input,
        RequestCheckHistoryImportPeer obj)
    {
        throw new NotImplementedException();
    }
}
