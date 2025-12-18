namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Transfer channel ownership
/// Possible errors
/// Code Type Description
/// 400 CHANNELS_ADMIN_PUBLIC_TOO_MUCH You're admin of too many public channels, make some channels private to change the username of this channel.
/// 400 CHANNEL_MONOFORUM_UNSUPPORTED <a href="https://corefork.telegram.org/api/channel#monoforums">Monoforums</a> do not support this feature.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_MEMBER_ADD_FAILED Could not add participants.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 PASSWORD_HASH_INVALID The provided password hash is invalid.
/// 400 PASSWORD_MISSING You must <a href="https://corefork.telegram.org/api/srp">enable 2FA</a> before executing this operation.
/// 400 PASSWORD_TOO_FRESH_%d The password was modified less than 24 hours ago, try again in %d seconds.
/// 400 SESSION_TOO_FRESH_%d This session was created less than 24 hours ago, try again in %d seconds.
/// 400 SRP_ID_INVALID Invalid SRP ID provided.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// 403 USER_PRIVACY_RESTRICTED The user's privacy settings do not allow you to do this.
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.editCreator"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class EditCreatorHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestEditCreator, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Channels.RequestEditCreator obj)
    {
        return Task.FromResult<MyTelegram.Schema.IUpdates>(new TUpdates { Users = [], Updates = [], Chats = [], Date = CurrentDate });
    }
}