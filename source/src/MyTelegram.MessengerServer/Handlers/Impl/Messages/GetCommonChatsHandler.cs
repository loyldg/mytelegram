using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetCommonChatsHandler : RpcResultObjectHandler<RequestGetCommonChats, IChats>,
    IGetCommonChatsHandler
{
    protected override Task<IChats> HandleCoreAsync(IRequestInput input,
        RequestGetCommonChats obj)
    {
        throw new NotImplementedException();
    }
}
