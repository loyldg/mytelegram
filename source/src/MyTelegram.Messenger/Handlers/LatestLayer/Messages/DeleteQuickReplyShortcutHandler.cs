namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.deleteQuickReplyShortcut" />
///</summary>
internal sealed class DeleteQuickReplyShortcutHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteQuickReplyShortcut, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestDeleteQuickReplyShortcut obj)
    {
        throw new NotImplementedException();
    }
}
