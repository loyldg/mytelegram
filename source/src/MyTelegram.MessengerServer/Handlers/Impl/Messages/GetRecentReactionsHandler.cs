// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class GetRecentReactionsHandler :
    RpcResultObjectHandler<Schema.Messages.RequestGetRecentReactions, Schema.Messages.IReactions>,
    Messages.IGetRecentReactionsHandler, IProcessedHandler
{
    protected override Task<Schema.Messages.IReactions> HandleCoreAsync(IRequestInput input,
        Schema.Messages.RequestGetRecentReactions obj)
    {
        return Task.FromResult<IReactions>(new TReactionsNotModified());
    }
}