using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class DeleteScheduledMessagesHandler : RpcResultObjectHandler<RequestDeleteScheduledMessages, IUpdates>,
    IDeleteScheduledMessagesHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestDeleteScheduledMessages obj)
    {
        throw new NotImplementedException();
    }
}