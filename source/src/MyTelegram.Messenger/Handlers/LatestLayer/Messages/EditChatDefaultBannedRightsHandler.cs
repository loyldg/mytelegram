namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Edit the default banned rights of a <a href="https://corefork.telegram.org/api/channel">channel/supergroup/group</a>.
/// Possible errors
/// Code Type Description
/// 400 BANNED_RIGHTS_INVALID You provided some invalid flags in the banned rights.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 UNTIL_DATE_INVALID Invalid until date provided.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.editChatDefaultBannedRights"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class EditChatDefaultBannedRightsHandler(ICommandBus commandBus, IChannelAdminRightsChecker channelAdminRightsChecker, IPeerHelper peerHelper, IAccessHashHelper accessHashHelper) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestEditChatDefaultBannedRights, MyTelegram.Schema.IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input, RequestEditChatDefaultBannedRights obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Peer);
        var peer = peerHelper.GetPeer(obj.Peer, input.UserId);
        switch (peer.PeerType)
        {
            case PeerType.Channel:
            {
                await channelAdminRightsChecker.CheckAdminRightAsync(peer.PeerId, input.UserId, p => p.BanUsers);
                var command = new EditChannelDefaultBannedRightsCommand(ChannelId.Create(peer.PeerId), input.ToRequestInfo(), GetChatBannedRights(obj.BannedRights), input.UserId);
                await commandBus.PublishAsync(command);
            }

                break;
        }

        return null !;
    }

    private ChatBannedRights GetChatBannedRights(IChatBannedRights chatBannedRights)
    {
        return ChatBannedRights.FromValue(chatBannedRights.Flags, chatBannedRights.UntilDate);
    }
}