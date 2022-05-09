using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ReadMentionsHandler : RpcResultObjectHandler<RequestReadMentions, IAffectedHistory>,
    IReadMentionsHandler
{
    protected override Task<IAffectedHistory> HandleCoreAsync(IRequestInput input,
        RequestReadMentions obj)
    {
        throw new NotImplementedException();
    }
}
