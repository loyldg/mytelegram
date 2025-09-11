namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Leave a <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHANNEL_PUBLIC_GROUP_NA channel/supergroup not available.
/// 400 CHAT_INVALID Invalid chat.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// 400 USER_CREATOR For channels.editAdmin: you've tried to edit the admin rights of the owner, but you're not the owner; for channels.leaveChannel: you can't leave this channel, because you're its creator.
/// 400 USER_NOT_PARTICIPANT You're not a member of this supergroup/channel.
/// See <a href="https://corefork.telegram.org/method/channels.leaveChannel" />
///</summary>
internal sealed class LeaveChannelHandler(
    IPeerHelper peerHelper,
    ICommandBus commandBus,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestLeaveChannel, MyTelegram.Schema.IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestLeaveChannel obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(input, inputChannel.ChannelId, inputChannel.AccessHash, AccessHashType.Channel);
            var channel = peerHelper.GetChannel(obj.Channel);
            var command = new LeaveChannelCommand(ChannelMemberId.Create(channel.PeerId, input.UserId),
                input.ToRequestInfo(),
                channel.PeerId,
                input.UserId);
            await commandBus.PublishAsync(command);

            return null!;
        }
        throw new NotImplementedException();
    }
}
