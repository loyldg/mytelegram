using MyTelegram.Handlers.Folders;
using MyTelegram.Schema.Folders;

namespace MyTelegram.MessengerServer.Handlers.Impl.Folders;

public class EditPeerFoldersHandler : RpcResultObjectHandler<RequestEditPeerFolders, IUpdates>,
    IEditPeerFoldersHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestEditPeerFolders obj)
    {
        throw new NotImplementedException();
    }
}