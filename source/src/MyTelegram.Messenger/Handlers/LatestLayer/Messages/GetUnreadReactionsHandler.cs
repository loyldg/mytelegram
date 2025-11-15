namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get unread reactions to messages you sent
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getUnreadReactions"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetUnreadReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetUnreadReactions, MyTelegram.Schema.Messages.IMessages>
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetUnreadReactions obj)
    {
        return Task.FromResult<IMessages>(new TMessages { Chats = [], Messages = [], Users = [], Topics = [] });
    }
}