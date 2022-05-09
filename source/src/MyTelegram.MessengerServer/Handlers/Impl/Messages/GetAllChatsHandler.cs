using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetAllChatsHandler : RpcResultObjectHandler<RequestGetAllChats, IChats>,
    IGetAllChatsHandler
{
    protected override Task<IChats> HandleCoreAsync(IRequestInput input,
        RequestGetAllChats obj)
    {
        throw new NotImplementedException();
    }
}
