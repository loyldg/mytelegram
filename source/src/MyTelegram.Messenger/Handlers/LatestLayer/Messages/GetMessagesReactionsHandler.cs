namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get <a href="https://corefork.telegram.org/api/reactions">message reactions »</a>
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getMessagesReactions"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetMessagesReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetMessagesReactions, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetMessagesReactions obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates { Updates = new(), Chats = new(), Users = new(), Date = CurrentDate, });
    }
}