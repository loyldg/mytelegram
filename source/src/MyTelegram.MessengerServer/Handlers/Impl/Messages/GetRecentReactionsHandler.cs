// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class GetRecentReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetRecentReactions, MyTelegram.Schema.Messages.IReactions>,
    Messages.IGetRecentReactionsHandler, IProcessedHandler
{
    protected override Task<MyTelegram.Schema.Messages.IReactions> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetRecentReactions obj)
    {
        return Task.FromResult<IReactions>(new TReactionsNotModified());
    }
}
