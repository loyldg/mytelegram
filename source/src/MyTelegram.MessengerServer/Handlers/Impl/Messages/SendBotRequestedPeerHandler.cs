// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

internal sealed class SendBotRequestedPeerHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendBotRequestedPeer, MyTelegram.Schema.IUpdates>,
    Messages.ISendBotRequestedPeerHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSendBotRequestedPeer obj)
    {
        throw new NotImplementedException();
    }
}
