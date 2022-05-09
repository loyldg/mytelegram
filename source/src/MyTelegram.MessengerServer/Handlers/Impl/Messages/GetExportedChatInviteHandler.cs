using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;
using IExportedChatInvite = MyTelegram.Schema.Messages.IExportedChatInvite;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetExportedChatInviteHandler : RpcResultObjectHandler<RequestGetExportedChatInvite, IExportedChatInvite>,
    IGetExportedChatInviteHandler
{
    protected override Task<IExportedChatInvite> HandleCoreAsync(IRequestInput input,
        RequestGetExportedChatInvite obj)
    {
        throw new NotImplementedException();
    }
}
