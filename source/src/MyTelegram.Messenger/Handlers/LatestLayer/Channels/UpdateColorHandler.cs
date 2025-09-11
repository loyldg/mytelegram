namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Update the <a href="https://corefork.telegram.org/api/colors">accent color and background custom emoji »</a> of a channel.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOOSTS_REQUIRED The specified channel must first be <a href="https://corefork.telegram.org/api/boost">boosted by its users</a> in order to perform this action.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.updateColor" />
///</summary>
internal sealed class UpdateColorHandler(
    ICommandBus commandBus,
    IPeerHelper peerHelper,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestUpdateColor, MyTelegram.Schema.IUpdates>
{
    protected override async Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestUpdateColor obj)
    {
        var channel = peerHelper.GetChannel(obj.Channel);
        await accessHashHelper.CheckAccessHashAsync(input, obj.Channel);

        var color = new PeerColor(obj.Color, obj.BackgroundEmojiId);
        var command = new UpdateChannelColorCommand(ChannelId.Create(channel.PeerId), input.ToRequestInfo(), color, obj.BackgroundEmojiId, obj.ForProfile);
        await commandBus.PublishAsync(command);

        return null!;
    }
}
