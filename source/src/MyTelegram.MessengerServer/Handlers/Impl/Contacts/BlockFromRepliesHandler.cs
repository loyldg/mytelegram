using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class BlockFromRepliesHandler : RpcResultObjectHandler<RequestBlockFromReplies, IUpdates>,
    IBlockFromRepliesHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestBlockFromReplies obj)
    {
        throw new NotImplementedException();
    }
}
