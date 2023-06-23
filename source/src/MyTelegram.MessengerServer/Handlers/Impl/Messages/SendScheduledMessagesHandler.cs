using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SendScheduledMessagesHandler : RpcResultObjectHandler<RequestSendScheduledMessages, IUpdates>,
    ISendScheduledMessagesHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestSendScheduledMessages obj)
    {
        throw new NotImplementedException();
    }
}