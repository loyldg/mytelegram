using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetChatsHandler : RpcResultObjectHandler<RequestGetChats, IChats>,
    IGetChatsHandler
{
    protected override Task<IChats> HandleCoreAsync(IRequestInput input,
        RequestGetChats obj)
    {
        throw new NotImplementedException();
    }
}