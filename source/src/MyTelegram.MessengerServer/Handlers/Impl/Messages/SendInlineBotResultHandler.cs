using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SendInlineBotResultHandler : RpcResultObjectHandler<RequestSendInlineBotResult, IUpdates>,
    ISendInlineBotResultHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestSendInlineBotResult obj)
    {
        throw new NotImplementedException();
    }
}