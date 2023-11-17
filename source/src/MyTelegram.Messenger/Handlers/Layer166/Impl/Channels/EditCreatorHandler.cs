// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Transfer channel ownership
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNELS_ADMIN_PUBLIC_TOO_MUCH You're admin of too many public channels, make some channels private to change the username of this channel.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 PASSWORD_HASH_INVALID The provided password hash is invalid.
/// 400 PASSWORD_MISSING You must enable 2FA in order to transfer ownership of a channel.
/// 400 PASSWORD_TOO_FRESH_%d The password was modified less than 24 hours ago, try again in %d seconds.
/// 400 SESSION_TOO_FRESH_%d This session was created less than 24 hours ago, try again in %d seconds.
/// 400 SRP_ID_INVALID Invalid SRP ID provided.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.editCreator" />
///</summary>
internal sealed class EditCreatorHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestEditCreator, MyTelegram.Schema.IUpdates>,
    Channels.IEditCreatorHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestEditCreator obj)
    {
        throw new NotImplementedException();
    }
}
