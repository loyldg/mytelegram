// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Leave a <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHANNEL_PUBLIC_GROUP_NA channel/supergroup not available.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// 400 USER_CREATOR You can't leave this channel, because you're its creator.
/// 400 USER_NOT_PARTICIPANT You're not a member of this supergroup/channel.
/// See <a href="https://corefork.telegram.org/method/channels.leaveChannel" />
///</summary>
internal sealed class LeaveChannelHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestLeaveChannel, MyTelegram.Schema.IUpdates>,
    Channels.ILeaveChannelHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    public LeaveChannelHandler(IPeerHelper peerHelper,
        ICommandBus commandBus,
        IAccessHashHelper accessHashHelper)
    {
        _peerHelper = peerHelper;
        _commandBus = commandBus;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestLeaveChannel obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash)
                ;

            var channel = _peerHelper.GetChannel(obj.Channel);
            var command = new LeaveChannelCommand(ChannelMemberId.Create(channel.PeerId, input.UserId),
                input.ToRequestInfo(),
                channel.PeerId,
                input.UserId);
            await _commandBus.PublishAsync(command, default);

            return null!;
        }
        throw new NotImplementedException();
    }
}
