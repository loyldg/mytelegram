namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Pin/unpin a dialog
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 PEER_HISTORY_EMPTY You can't pin an empty chat with a user.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 PINNED_DIALOGS_TOO_MUCH Too many pinned dialogs.
/// See <a href="https://corefork.telegram.org/method/messages.toggleDialogPin" />
///</summary>
internal sealed class ToggleDialogPinHandler(
    ICommandBus commandBus,
    IPeerHelper peerHelper,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleDialogPin, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestToggleDialogPin obj)
    {
        switch (obj.Peer)
        {
            case TInputDialogPeer inputDialogPeer:
                await accessHashHelper.CheckAccessHashAsync(input, inputDialogPeer.Peer);
                var peer = peerHelper.GetPeer(inputDialogPeer.Peer, input.UserId);
                //var ownerUid = peer.PeerType == PeerType.Channel ? peer.PeerId : input.UserId;
                var command =
                    new ToggleDialogPinnedCommand(DialogId.Create(input.UserId, peer), input.ToRequestInfo(), obj.Pinned);
                await commandBus.PublishAsync(command, CancellationToken.None);
                return null!;
            case TInputDialogPeerFolder:
                return new TBoolTrue();
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
