using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class MarkDialogUnreadHandler : RpcResultObjectHandler<RequestMarkDialogUnread, IBool>,
    IMarkDialogUnreadHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;

    public MarkDialogUnreadHandler(ICommandBus commandBus,
        IPeerHelper peerHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestMarkDialogUnread obj)
    {
        switch (obj.Peer)
        {
            case TInputDialogPeer inputDialogPeer:
                var peer = _peerHelper.GetPeer(inputDialogPeer.Peer, input.UserId);
                var command =
                    new MarkDialogAsUnreadCommand(DialogId.Create(input.UserId, peer), input.ReqMsgId, obj.Unread);
                await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);
                break;
            case TInputDialogPeerFolder inputDialogPeerFolder:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return new TBoolTrue();
    }
}
