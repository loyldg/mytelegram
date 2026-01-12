namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Ban/unban/kick a user in a <a href="https://corefork.telegram.org/api/channel">supergroup/channel</a>.
/// Possible errors
/// Code Type Description
/// 406 BANNED_RIGHTS_INVALID You provided some invalid flags in the banned rights.
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
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.editBanned"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class EditBannedHandler(IPeerHelper peerHelper, ICommandBus commandBus,
    IChannelAppService channelAppService,
    IChannelAdminRightsChecker channelAdminRightsChecker) : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestEditBanned, MyTelegram.Schema.IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input, RequestEditBanned obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            var channel = peerHelper.GetChannel(obj.Channel);
            var peer = peerHelper.GetPeer(obj.Participant);
            var channelReadModel = await channelAppService.GetAsync(channel.PeerId);

            await channelAdminRightsChecker.CheckAdminRightAsync(inputChannel.ChannelId, input.UserId, p => p.BanUsers && peer.PeerId != channelReadModel.CreatorId, RpcErrors.RpcErrors400.ChatAdminRequired);

            if (obj.BannedRights.SendPlain)
            {
                obj.BannedRights.SendMessages = true;
                obj.BannedRights.Flags.SetBit(1);
            }

            var bannedRights = ChatBannedRights.FromValue(obj.BannedRights.Flags, obj.BannedRights.UntilDate);
            var command = new EditBannedCommand(ChannelMemberId.Create(channel.PeerId, peer.PeerId), input.ToRequestInfo(), input.UserId, channel.PeerId, peer.PeerId, bannedRights);
            await commandBus.PublishAsync(command);
            return null!;
        }

        throw new NotImplementedException();
    }
}