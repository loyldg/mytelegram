namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Pin or unpin a <a href="https://corefork.telegram.org/api/saved-messages">saved message dialog »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.toggleSavedDialogPin" />
///</summary>
internal sealed class ToggleSavedDialogPinHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleSavedDialogPin, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestToggleSavedDialogPin obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
