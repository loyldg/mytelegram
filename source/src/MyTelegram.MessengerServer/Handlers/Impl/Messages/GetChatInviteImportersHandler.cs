using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetChatInviteImportersHandler :
    RpcResultObjectHandler<RequestGetChatInviteImporters, IChatInviteImporters>,
    IGetChatInviteImportersHandler
{
    protected override Task<IChatInviteImporters> HandleCoreAsync(IRequestInput input,
        RequestGetChatInviteImporters obj)
    {
        throw new NotImplementedException();
    }
}
