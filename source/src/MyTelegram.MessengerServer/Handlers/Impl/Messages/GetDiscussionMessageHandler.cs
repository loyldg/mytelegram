using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetDiscussionMessageHandler : RpcResultObjectHandler<RequestGetDiscussionMessage, IDiscussionMessage>,
    IGetDiscussionMessageHandler
{
    protected override Task<IDiscussionMessage> HandleCoreAsync(IRequestInput input,
        RequestGetDiscussionMessage obj)
    {
        throw new NotImplementedException();
    }
}
