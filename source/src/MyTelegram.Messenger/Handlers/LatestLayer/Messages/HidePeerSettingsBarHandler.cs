using MyTelegram.Domain.Aggregates.PeerSetting;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Should be called after the user hides the <a href="https://corefork.telegram.org/api/action-bar">report spam/add as contact bar</a> of a new chat, effectively prevents the user from executing the actions specified in the <a href="https://corefork.telegram.org/api/action-bar">action bar »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.hidePeerSettingsBar" />
///</summary>
internal sealed class HidePeerSettingsBarHandler(IPeerHelper peerHelper, ICommandBus commandBus)
    : RpcResultObjectHandler<RequestHidePeerSettingsBar, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestHidePeerSettingsBar obj)
    {
        var peer = peerHelper.GetPeer(obj.Peer);
        var command = new HidePeerSettingsBarCommand(PeerSettingsId.Create(input.UserId, peer.PeerId),
            input.ToRequestInfo(), peer.PeerId);
        await commandBus.PublishAsync(command);

        return new TBoolTrue();
    }
}
