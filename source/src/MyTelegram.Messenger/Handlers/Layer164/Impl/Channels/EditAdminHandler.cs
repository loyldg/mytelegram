// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Modify the admin rights of a user in a <a href="https://corefork.telegram.org/api/channel">supergroup/channel</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ADMINS_TOO_MUCH There are too many admins.
/// 400 ADMIN_RANK_EMOJI_NOT_ALLOWED An admin rank cannot contain emojis.
/// 400 ADMIN_RANK_INVALID The specified admin rank is invalid.
/// 400 BOTS_TOO_MUCH There are too many bots in this chat/channel.
/// 400 BOT_CHANNELS_NA Bots can't edit admin privileges.
/// 400 BOT_GROUPS_BLOCKED This bot can't be added to groups.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_INVITE_REQUIRED You do not have the rights to do this.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 406 FRESH_CHANGE_ADMINS_FORBIDDEN You were just elected admin, you can't add or modify other admins yet.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 403 RIGHT_FORBIDDEN Your admin rights do not allow you to do this.
/// 400 USERS_TOO_MUCH The maximum number of users has been exceeded (to create a chat, for example).
/// 400 USER_BLOCKED User blocked.
/// 403 USER_CHANNELS_TOO_MUCH One of the users you tried to add is already in too many channels/supergroups.
/// 400 USER_CREATOR You can't leave this channel, because you're its creator.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// 403 USER_NOT_MUTUAL_CONTACT The provided user is not a mutual contact.
/// 403 USER_PRIVACY_RESTRICTED The user's privacy settings do not allow you to do this.
/// 403 USER_RESTRICTED You're spamreported, you can't create channels or chats.
/// See <a href="https://corefork.telegram.org/method/channels.editAdmin" />
///</summary>
internal sealed class EditAdminHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestEditAdmin, MyTelegram.Schema.IUpdates>,
    Channels.IEditAdminHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    public EditAdminHandler(ICommandBus commandBus,
        IPeerHelper peerHelper,
        IAccessHashHelper accessHashHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestEditAdmin obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash);

            var peer = _peerHelper.GetPeer(obj.UserId);
            var isBot = _peerHelper.IsBotUser(peer.PeerId);
            var command = new EditChannelAdminCommand(ChannelId.Create(inputChannel.ChannelId),
                input.ToRequestInfo(),
                input.UserId,
                false,
                peer.PeerId,
                isBot,
                new ChatAdminRights(obj.AdminRights.Flags),
                obj.Rank,
                CurrentDate
            );
            await _commandBus.PublishAsync(command, CancellationToken.None);
            return null!;
        }

        throw new NotImplementedException();
    }
}
