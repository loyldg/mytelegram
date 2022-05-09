using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class DeleteRevokedExportedChatInvitesHandler :
    RpcResultObjectHandler<RequestDeleteRevokedExportedChatInvites, IBool>,
    IDeleteRevokedExportedChatInvitesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestDeleteRevokedExportedChatInvites obj)
    {
        throw new NotImplementedException();
    }
}
