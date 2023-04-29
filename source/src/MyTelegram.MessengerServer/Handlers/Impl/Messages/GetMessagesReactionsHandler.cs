// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class GetMessagesReactionsHandler : RpcResultObjectHandler<RequestGetMessagesReactions, Schema.IUpdates>,
    Messages.IGetMessagesReactionsHandler, IProcessedHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestGetMessagesReactions obj)
    {
        var updates = new Schema.TUpdates
        {
            Updates = new TVector<IUpdate>(),
            Chats = new TVector<IChat>(),
            Users = new TVector<IUser>()
        };
        return Task.FromResult<IUpdates>(updates);
    }
}
