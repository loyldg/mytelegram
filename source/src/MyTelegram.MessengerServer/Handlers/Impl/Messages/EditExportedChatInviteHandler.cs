using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;
using IExportedChatInvite = MyTelegram.Schema.Messages.IExportedChatInvite;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class EditExportedChatInviteHandler : RpcResultObjectHandler<RequestEditExportedChatInvite, IExportedChatInvite>,
    IEditExportedChatInviteHandler
{
    protected override Task<IExportedChatInvite> HandleCoreAsync(IRequestInput input,
        RequestEditExportedChatInvite obj)
    {
        throw new NotImplementedException();
    }
}
