// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class ClearRecentReactionsHandler : RpcResultObjectHandler<RequestClearRecentReactions, IBool>,
    Messages.IClearRecentReactionsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestClearRecentReactions obj)
    {
        throw new NotImplementedException();
    }
}