// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/reactions">message reactions »</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// See <a href="https://corefork.telegram.org/method/messages.getMessagesReactions" />
///</summary>
internal sealed class GetMessagesReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetMessagesReactions, MyTelegram.Schema.IUpdates>,
    Messages.IGetMessagesReactionsHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetMessagesReactions obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates
        {
            Updates = new(),
            Chats = new(),
            Users = new(),
            Date = CurrentDate,
        });
    }
}
