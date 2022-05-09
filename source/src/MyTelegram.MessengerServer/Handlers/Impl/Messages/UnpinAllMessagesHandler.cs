using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class UnpinAllMessagesHandler : RpcResultObjectHandler<RequestUnpinAllMessages, IAffectedHistory>,
    IUnpinAllMessagesHandler
{
    protected override Task<IAffectedHistory> HandleCoreAsync(IRequestInput input,
        RequestUnpinAllMessages obj)
    {
        throw new NotImplementedException();
    }
}
