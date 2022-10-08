// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class GetTopReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetTopReactions, MyTelegram.Schema.Messages.IReactions>,
    Messages.IGetTopReactionsHandler, IProcessedHandler
{
    protected override Task<MyTelegram.Schema.Messages.IReactions> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetTopReactions obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IReactions>(new TReactionsNotModified());
    }
}
