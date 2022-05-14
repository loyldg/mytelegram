// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class ReadReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReadReactions, MyTelegram.Schema.Messages.IAffectedHistory>,
    Messages.IReadReactionsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedHistory> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReadReactions obj)
    {
        throw new NotImplementedException();
    }
}
