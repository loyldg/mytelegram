using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class DeleteExportedChatInviteHandler : RpcResultObjectHandler<RequestDeleteExportedChatInvite, IBool>,
    IDeleteExportedChatInviteHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestDeleteExportedChatInvite obj)
    {
        throw new NotImplementedException();
    }
}
