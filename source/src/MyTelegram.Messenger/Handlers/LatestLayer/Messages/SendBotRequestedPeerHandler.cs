namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Send one or more chosen peers, as requested by a <a href="https://corefork.telegram.org/constructor/keyboardButtonRequestPeer">keyboardButtonRequestPeer</a> button.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.sendBotRequestedPeer" />
///</summary>
internal sealed class SendBotRequestedPeerHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendBotRequestedPeer, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSendBotRequestedPeer obj)
    {
        throw new NotImplementedException();
    }
}
