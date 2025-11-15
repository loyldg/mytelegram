namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Send a <a href="https://corefork.telegram.org/api/business#quick-reply-shortcuts">quick reply shortcut »</a>.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 403 PREMIUM_ACCOUNT_REQUIRED A premium account is required to execute this action.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.sendQuickReplyMessages"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SendQuickReplyMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendQuickReplyMessages, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSendQuickReplyMessages obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates { Updates = [], Chats = [], Users = [], Date = CurrentDate });
    }
}