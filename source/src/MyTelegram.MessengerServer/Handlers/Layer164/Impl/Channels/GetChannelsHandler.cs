using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class GetChannelsHandler : RpcResultObjectHandler<RequestGetChannels, IChats>,
    IGetChannelsHandler
{
    protected override Task<IChats> HandleCoreAsync(IRequestInput input,
        RequestGetChannels obj)
    {
        throw new NotImplementedException();
    }
}