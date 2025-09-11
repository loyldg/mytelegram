namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Hide/unhide message history for new channel/supergroup users
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_LINK_EXISTS The chat is public, you can't hide the history to new users.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 FORUM_ENABLED You can't execute the specified action because the group is a <a href="https://corefork.telegram.org/api/forum">forum</a>, disable forum functionality to continue.
/// See <a href="https://corefork.telegram.org/method/channels.togglePreHistoryHidden" />
///</summary>
internal sealed class TogglePreHistoryHiddenHandler(
    ICommandBus commandBus,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestTogglePreHistoryHidden, MyTelegram.Schema.IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestTogglePreHistoryHidden obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(input, inputChannel.ChannelId, inputChannel.AccessHash, AccessHashType.Channel);

            var command = new TogglePreHistoryHiddenCommand(ChannelId.Create(inputChannel.ChannelId),
                input.ToRequestInfo(),
                obj.Enabled,
                input.UserId);
            await commandBus.PublishAsync(command);
            return null!;
        }

        throw new NotImplementedException();
    }
}
