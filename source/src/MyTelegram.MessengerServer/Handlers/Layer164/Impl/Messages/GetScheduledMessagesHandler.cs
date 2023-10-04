using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetScheduledMessagesHandler : RpcResultObjectHandler<RequestGetScheduledMessages, IMessages>,
    IGetScheduledMessagesHandler
{
    protected override Task<IMessages> HandleCoreAsync(IRequestInput input,
        RequestGetScheduledMessages obj)
    {
        throw new NotImplementedException();
    }
}