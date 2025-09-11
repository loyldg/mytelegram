namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.editQuickReplyShortcut" />
///</summary>
internal sealed class EditQuickReplyShortcutHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestEditQuickReplyShortcut, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestEditQuickReplyShortcut obj)
    {
        throw new NotImplementedException();
    }
}
