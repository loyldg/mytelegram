// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

internal sealed class SendBotRequestedPeerHandler :
    RpcResultObjectHandler<RequestSendBotRequestedPeer, Schema.IUpdates>,
    Messages.ISendBotRequestedPeerHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestSendBotRequestedPeer obj)
    {
        throw new NotImplementedException();
    }
}