namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Update the <a href="https://corefork.telegram.org/api/colors">accent color and background custom emoji »</a> of a channel.
/// Possible errors
/// Code Type Description
/// 400 BOOSTS_REQUIRED The specified channel must first be <a href="https://corefork.telegram.org/api/boost">boosted by its users</a> in order to perform this action.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.updateColor"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateColorHandler(ICommandBus commandBus, IPeerHelper peerHelper, IChannelAdminRightsChecker channelAdminRightsChecker, IAccessHashHelper accessHashHelper) : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestUpdateColor, MyTelegram.Schema.IUpdates>
{
    protected override async Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Channels.RequestUpdateColor obj)
    {
        var channel = peerHelper.GetChannel(obj.Channel);
        await accessHashHelper.CheckAccessHashAsync(input, obj.Channel);
        await channelAdminRightsChecker.CheckAdminRightAsync(channel.PeerId, input.UserId, p => p.PinMessages, RpcErrors.RpcErrors400.ChatAdminRequired);
        var color = new PeerColor(obj.Color, obj.BackgroundEmojiId);
        var command = new UpdateChannelColorCommand(ChannelId.Create(channel.PeerId), input.ToRequestInfo(), color, obj.BackgroundEmojiId, obj.ForProfile);
        await commandBus.PublishAsync(command);
        return null !;
    }
}