using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ToggleDialogPinHandler : RpcResultObjectHandler<RequestToggleDialogPin, IBool>,
    IToggleDialogPinHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;

    public ToggleDialogPinHandler(ICommandBus commandBus,
        IPeerHelper peerHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestToggleDialogPin obj)
    {
        switch (obj.Peer)
        {
            case TInputDialogPeer inputDialogPeer:
                var peer = _peerHelper.GetPeer(inputDialogPeer.Peer, input.UserId);
                //var ownerPeerId = peer.PeerType == PeerType.Channel ? peer.PeerId : input.UserId;
                var command =
                    new ToggleDialogPinnedCommand(DialogId.Create(input.UserId, peer), input.ReqMsgId, obj.Pinned);
                await _commandBus.PublishAsync(command, CancellationToken.None);
                return null!;
            case TInputDialogPeerFolder:
                return new TBoolTrue();
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
