namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get <a href="https://corefork.telegram.org/api/reactions">message reaction</a> list, along with the sender of each reaction.
/// Possible errors
/// Code Type Description
/// 403 BROADCAST_FORBIDDEN Channel poll voters and reactions cannot be fetched to prevent deanonymization.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getMessageReactionsList"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetMessageReactionsListHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetMessageReactionsList, MyTelegram.Schema.Messages.IMessageReactionsList>
{
    protected override Task<MyTelegram.Schema.Messages.IMessageReactionsList> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetMessageReactionsList obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IMessageReactionsList>(new TMessageReactionsList { Chats = new(), Reactions = new(), Users = new(), });
    }
}