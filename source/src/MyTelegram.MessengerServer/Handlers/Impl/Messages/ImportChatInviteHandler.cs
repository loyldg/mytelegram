using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ImportChatInviteHandler : RpcResultObjectHandler<RequestImportChatInvite, IUpdates>,
    IImportChatInviteHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestImportChatInvite obj)
    {
        throw new NotImplementedException();
    }
}
