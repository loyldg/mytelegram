// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Manually mark dialog as unread
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.markDialogUnread" />
///</summary>
internal sealed class MarkDialogUnreadHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestMarkDialogUnread, IBool>,
    Messages.IMarkDialogUnreadHandler
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
                    new MarkDialogAsUnreadCommand(DialogId.Create(input.UserId, peer), input.ToRequestInfo(), obj.Unread);
                await _commandBus.PublishAsync(command, CancellationToken.None);
                break;
            case TInputDialogPeerFolder inputDialogPeerFolder:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return new TBoolTrue();
    }
}
