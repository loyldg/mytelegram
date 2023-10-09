// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Pin/unpin a dialog
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 PEER_HISTORY_EMPTY You can't pin an empty chat with a user.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 PINNED_DIALOGS_TOO_MUCH Too many pinned dialogs.
/// See <a href="https://corefork.telegram.org/method/messages.toggleDialogPin" />
///</summary>
internal sealed class ToggleDialogPinHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleDialogPin, IBool>,
    Messages.IToggleDialogPinHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestToggleDialogPin obj)
    {
        throw new NotImplementedException();
    }
}
