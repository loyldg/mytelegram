// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Send a chosen peer, as requested by a <a href="https://corefork.telegram.org/constructor/keyboardButtonRequestPeer">keyboardButtonRequestPeer</a> button.
/// See <a href="https://corefork.telegram.org/method/messages.sendBotRequestedPeer" />
///</summary>
internal sealed class SendBotRequestedPeerHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendBotRequestedPeer, MyTelegram.Schema.IUpdates>,
    Messages.ISendBotRequestedPeerHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSendBotRequestedPeer obj)
    {
        throw new NotImplementedException();
    }
}
