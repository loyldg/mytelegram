namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Toggle supergroup slow mode: if enabled, users will only be able to send one message every <code>seconds</code> seconds
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 SECONDS_INVALID Invalid duration provided.
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.toggleSlowMode"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleSlowModeHandler(ICommandBus commandBus, IChannelAdminRightsChecker channelAdminRightsChecker) : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleSlowMode, MyTelegram.Schema.IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input, RequestToggleSlowMode obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await channelAdminRightsChecker.CheckAdminRightAsync(obj.Channel, input.UserId, p => p.ChangeInfo);
            var command = new ToggleSlowModeCommand(ChannelId.Create(inputChannel.ChannelId), input.ToRequestInfo(), obj.Seconds, input.UserId);
            await commandBus.PublishAsync(command, CancellationToken.None);
            return null!;
        }

        throw new NotImplementedException();
    }
}