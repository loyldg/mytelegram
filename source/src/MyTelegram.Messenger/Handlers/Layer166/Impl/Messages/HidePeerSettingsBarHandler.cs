// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Should be called after the user hides the report spam/add as contact bar of a new chat, effectively prevents the user from executing the actions specified in the <a href="https://corefork.telegram.org/constructor/peerSettings">peer's settings</a>.
/// See <a href="https://corefork.telegram.org/method/messages.hidePeerSettingsBar" />
///</summary>
internal sealed class HidePeerSettingsBarHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestHidePeerSettingsBar, IBool>,
    Messages.IHidePeerSettingsBarHandler
{
    private readonly IPeerHelper _peerHelper;
    private readonly ICommandBus _commandBus;

    public HidePeerSettingsBarHandler(IPeerHelper peerHelper, ICommandBus commandBus)
    {
        _peerHelper = peerHelper;
        _commandBus = commandBus;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestHidePeerSettingsBar obj)
    {
        var peer = _peerHelper.GetPeer(obj.Peer);
        var command = new HidePeerSettingsBarCommand(PeerSettingsId.Create(input.UserId, peer.PeerId),
            input.ToRequestInfo(), peer.PeerId);
        await _commandBus.PublishAsync(command, default);

        return new TBoolTrue();
    }
}
