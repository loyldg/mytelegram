using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class ReadMessageContentsHandler : RpcResultObjectHandler<RequestReadMessageContents, IBool>,
    IReadMessageContentsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReadMessageContents obj)
    {
        throw new NotImplementedException();
    }
}
