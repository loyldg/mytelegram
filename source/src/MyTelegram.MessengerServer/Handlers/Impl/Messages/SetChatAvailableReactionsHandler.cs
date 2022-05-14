// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class SetChatAvailableReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetChatAvailableReactions, MyTelegram.Schema.IUpdates>,
    Messages.ISetChatAvailableReactionsHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSetChatAvailableReactions obj)
    {
        throw new NotImplementedException();
    }
}
