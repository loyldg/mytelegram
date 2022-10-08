// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class ClearRecentReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestClearRecentReactions, IBool>,
    Messages.IClearRecentReactionsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestClearRecentReactions obj)
    {
        throw new NotImplementedException();
    }
}
