// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class ReadReactionsHandler : RpcResultObjectHandler<RequestReadReactions, IAffectedHistory>,
    Messages.IReadReactionsHandler
{
    protected override Task<IAffectedHistory> HandleCoreAsync(IRequestInput input,
        RequestReadReactions obj)
    {
        throw new NotImplementedException();
    }
}