namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Show or hide the <a href="https://corefork.telegram.org/api/translation">real-time chat translation popup</a> for a certain chat
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.togglePeerTranslations" />
///</summary>
internal sealed class TogglePeerTranslationsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestTogglePeerTranslations, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestTogglePeerTranslations obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
        //throw new NotImplementedException();
    }
}
