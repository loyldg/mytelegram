using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class DeleteChannelHandler : RpcResultObjectHandler<RequestDeleteChannel, IUpdates>,
    IDeleteChannelHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestDeleteChannel obj)
    {
        throw new NotImplementedException();
    }
}
