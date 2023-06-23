// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class SetChatAvailableReactionsHandler :
    RpcResultObjectHandler<RequestSetChatAvailableReactions, Schema.IUpdates>,
    Messages.ISetChatAvailableReactionsHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestSetChatAvailableReactions obj)
    {
        throw new NotImplementedException();
    }
}