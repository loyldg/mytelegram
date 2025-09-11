namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Manually mark dialog as unread
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.markDialogUnread" />
///</summary>
internal sealed class MarkDialogUnreadHandler(
    ICommandBus commandBus,
    IPeerHelper peerHelper) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestMarkDialogUnread, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestMarkDialogUnread obj)
    {
        switch (obj.Peer)
        {
            case TInputDialogPeer inputDialogPeer:
                var peer = peerHelper.GetPeer(inputDialogPeer.Peer, input.UserId);
                var command =
                    new MarkDialogAsUnreadCommand(DialogId.Create(input.UserId, peer), input.ToRequestInfo(), obj.Unread);
                await commandBus.PublishAsync(command, CancellationToken.None);
                break;
            case TInputDialogPeerFolder inputDialogPeerFolder:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return new TBoolTrue();
    }
}
