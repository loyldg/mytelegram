using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class GetInactiveChannelsHandler : RpcResultObjectHandler<RequestGetInactiveChannels, IInactiveChats>,
    IGetInactiveChannelsHandler
{
    protected override Task<IInactiveChats> HandleCoreAsync(IRequestInput input,
        RequestGetInactiveChannels obj)
    {
        throw new NotImplementedException();
    }
}
