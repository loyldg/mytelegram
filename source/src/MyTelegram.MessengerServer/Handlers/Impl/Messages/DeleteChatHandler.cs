using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class DeleteChatHandler : RpcResultObjectHandler<RequestDeleteChat, IBool>,
    IDeleteChatHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestDeleteChat obj)
    {
        throw new NotImplementedException();
    }
}
