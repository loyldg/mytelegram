// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class GetTopReactionsHandler :
    RpcResultObjectHandler<Schema.Messages.RequestGetTopReactions, Schema.Messages.IReactions>,
    Messages.IGetTopReactionsHandler, IProcessedHandler
{
    protected override Task<Schema.Messages.IReactions> HandleCoreAsync(IRequestInput input,
        Schema.Messages.RequestGetTopReactions obj)
    {
        return Task.FromResult<Schema.Messages.IReactions>(new TReactionsNotModified());
    }
}