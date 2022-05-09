using MyTelegram.Handlers.Folders;
using MyTelegram.Schema.Folders;

namespace MyTelegram.MessengerServer.Handlers.Impl.Folders;

public class DeleteFolderHandler : RpcResultObjectHandler<RequestDeleteFolder, IUpdates>,
    IDeleteFolderHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestDeleteFolder obj)
    {
        throw new NotImplementedException();
    }
}
