using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class GetSponsoredMessagesHandler : RpcResultObjectHandler<RequestGetSponsoredMessages, ISponsoredMessages>,
    IGetSponsoredMessagesHandler, IProcessedHandler
{
    protected override Task<ISponsoredMessages> HandleCoreAsync(IRequestInput input,
        RequestGetSponsoredMessages obj)
    {
        return Task.FromResult<ISponsoredMessages>(new TSponsoredMessages
        {
            Chats = new TVector<IChat>(), Messages = new TVector<ISponsoredMessage>(), Users = new TVector<IUser>()
        });
    }
}
