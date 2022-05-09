using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class GetAdminedPublicChannelsHandler : RpcResultObjectHandler<RequestGetAdminedPublicChannels, IChats>,
    IGetAdminedPublicChannelsHandler
{
    protected override Task<IChats> HandleCoreAsync(IRequestInput input,
        RequestGetAdminedPublicChannels obj)
    {
        throw new NotImplementedException();
    }
}
