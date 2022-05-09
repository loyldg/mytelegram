using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SendVoteHandler : RpcResultObjectHandler<RequestSendVote, IUpdates>,
    ISendVoteHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestSendVote obj)
    {
        throw new NotImplementedException();
    }
}
