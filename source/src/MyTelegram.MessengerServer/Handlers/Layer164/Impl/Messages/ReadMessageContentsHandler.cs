using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;
using RequestReadMessageContents = MyTelegram.Schema.Messages.RequestReadMessageContents;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ReadMessageContentsHandler : RpcResultObjectHandler<RequestReadMessageContents, IAffectedMessages>,
    IReadMessageContentsHandler
{
    protected override Task<IAffectedMessages> HandleCoreAsync(IRequestInput input,
        RequestReadMessageContents obj)
    {
        throw new NotImplementedException();
    }
}