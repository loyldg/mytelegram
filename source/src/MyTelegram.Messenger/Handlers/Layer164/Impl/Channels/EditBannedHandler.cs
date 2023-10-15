// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Ban/unban/kick a user in a <a href="https://corefork.telegram.org/api/channel">supergroup/channel</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PARTICIPANT_ID_INVALID The specified participant ID is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_ADMIN_INVALID You're not an admin.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.editBanned" />
///</summary>
internal sealed class EditBannedHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestEditBanned, MyTelegram.Schema.IUpdates>,
    Channels.IEditBannedHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    public EditBannedHandler(IPeerHelper peerHelper,
        ICommandBus commandBus,
        IAccessHashHelper accessHashHelper)
    {
        _peerHelper = peerHelper;
        _commandBus = commandBus;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestEditBanned obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash);

            var channel = _peerHelper.GetChannel(obj.Channel);
            var peer = _peerHelper.GetPeer(obj.Participant);
            var bannedRights = ChatBannedRights.FromValue(obj.BannedRights.Flags.ToInt(), obj.BannedRights.UntilDate);
            var command = new EditBannedCommand(ChannelMemberId.Create(channel.PeerId, peer.PeerId),
                input.ToRequestInfo(),
                input.UserId,
                channel.PeerId,
                peer.PeerId,
                bannedRights);
            await _commandBus.PublishAsync(command, default);
            return null!;
        }

        throw new NotImplementedException();
    }
}
