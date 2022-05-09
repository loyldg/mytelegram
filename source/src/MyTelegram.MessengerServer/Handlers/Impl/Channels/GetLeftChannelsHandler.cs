using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class GetLeftChannelsHandler : RpcResultObjectHandler<RequestGetLeftChannels, IChats>,
    IGetLeftChannelsHandler
{
    protected override Task<IChats> HandleCoreAsync(IRequestInput input,
        RequestGetLeftChannels obj)
    {
        throw new NotImplementedException();
    }
}
