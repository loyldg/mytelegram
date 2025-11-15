namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Delete one or more messages from a <a href="https://corefork.telegram.org/api/business#quick-reply-shortcuts">quick reply shortcut</a>. This will also emit an <a href="https://corefork.telegram.org/constructor/updateDeleteQuickReplyMessages">updateDeleteQuickReplyMessages</a> update.
/// Possible errors
/// Code Type Description
/// 400 SHORTCUT_INVALID The specified shortcut is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.deleteQuickReplyMessages"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class DeleteQuickReplyMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteQuickReplyMessages, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestDeleteQuickReplyMessages obj)
    {
        throw new NotImplementedException();
    }
}