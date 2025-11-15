namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Completely delete a <a href="https://corefork.telegram.org/api/business#quick-reply-shortcuts">quick reply shortcut</a>.<br/>
/// This will also emit an <a href="https://corefork.telegram.org/constructor/updateDeleteQuickReply">updateDeleteQuickReply</a> update to other logged-in sessions (and <em>no</em> <a href="https://corefork.telegram.org/constructor/updateDeleteQuickReplyMessages">updateDeleteQuickReplyMessages</a> updates, even if all the messages in the shortcuts are also deleted by this method).
/// Possible errors
/// Code Type Description
/// 400 SHORTCUT_INVALID The specified shortcut is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.deleteQuickReplyShortcut"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class DeleteQuickReplyShortcutHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteQuickReplyShortcut, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestDeleteQuickReplyShortcut obj)
    {
        throw new NotImplementedException();
    }
}