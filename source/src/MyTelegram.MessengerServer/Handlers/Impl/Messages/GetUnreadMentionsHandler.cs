using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetUnreadMentionsHandler : RpcResultObjectHandler<RequestGetUnreadMentions, IMessages>,
    IGetUnreadMentionsHandler
{
    protected override Task<IMessages> HandleCoreAsync(IRequestInput input,
        RequestGetUnreadMentions obj)
    {
        throw new NotImplementedException();
    }
}
