// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class GetMessagesReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetMessagesReactions, MyTelegram.Schema.IUpdates>,
    Messages.IGetMessagesReactionsHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetMessagesReactions obj)
    {
        throw new NotImplementedException();
    }
}
