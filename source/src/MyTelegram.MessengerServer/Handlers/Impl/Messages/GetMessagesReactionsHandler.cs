// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class GetMessagesReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetMessagesReactions, MyTelegram.Schema.IUpdates>,
    Messages.IGetMessagesReactionsHandler, IProcessedHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetMessagesReactions obj)
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
