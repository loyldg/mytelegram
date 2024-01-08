// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// See <a href="https://corefork.telegram.org/method/channels.updateColor" />
///</summary>
internal sealed class UpdateColorHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestUpdateColor, MyTelegram.Schema.IUpdates>,
    Channels.IUpdateColorHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    public UpdateColorHandler(ICommandBus commandBus, IPeerHelper peerHelper, IAccessHashHelper accessHashHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
        _accessHashHelper = accessHashHelper;
    }
    protected override async Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestUpdateColor obj)
    {
        var channel = _peerHelper.GetChannel(obj.Channel);
        await _accessHashHelper.CheckAccessHashAsync(obj.Channel);

        var color = new PeerColor(obj.Color, obj.BackgroundEmojiId);
        var command = new UpdateChannelColorCommand(ChannelId.Create(channel.PeerId), input.ToRequestInfo(), color, obj.BackgroundEmojiId, obj.ForProfile);
        await _commandBus.PublishAsync(command, default);
        return null!;
    }
}
