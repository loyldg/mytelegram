using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetRepliesHandler : RpcResultObjectHandler<RequestGetReplies, IMessages>,
    IGetRepliesHandler
{
    protected override Task<IMessages> HandleCoreAsync(IRequestInput input,
        RequestGetReplies obj)
    {
        throw new NotImplementedException();
    }
}
