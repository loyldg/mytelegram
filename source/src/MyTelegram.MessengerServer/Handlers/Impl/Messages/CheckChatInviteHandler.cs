using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class CheckChatInviteHandler : RpcResultObjectHandler<RequestCheckChatInvite, IChatInvite>,
    ICheckChatInviteHandler
{
    protected override Task<IChatInvite> HandleCoreAsync(IRequestInput input,
        RequestCheckChatInvite obj)
    {
        throw new NotImplementedException();
    }
}
