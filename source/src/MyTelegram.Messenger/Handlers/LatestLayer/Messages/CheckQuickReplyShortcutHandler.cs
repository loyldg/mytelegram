namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Before offering the user the choice to add a message to a <a href="https://corefork.telegram.org/api/business#quick-reply-shortcuts">quick reply shortcut</a>, to make sure that none of the limits specified <a href="https://corefork.telegram.org/api/business#quick-reply-shortcuts">here »</a> were reached.
/// Possible errors
/// Code Type Description
/// 403 PREMIUM_ACCOUNT_REQUIRED A premium account is required to execute this action.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.checkQuickReplyShortcut"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class CheckQuickReplyShortcutHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestCheckQuickReplyShortcut, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestCheckQuickReplyShortcut obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}