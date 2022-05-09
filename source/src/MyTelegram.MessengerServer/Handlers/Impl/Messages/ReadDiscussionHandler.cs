using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ReadDiscussionHandler : RpcResultObjectHandler<RequestReadDiscussion, IBool>,
    IReadDiscussionHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReadDiscussion obj)
    {
        throw new NotImplementedException();
    }
}
