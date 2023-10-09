// ReSharper disable All

namespace MyTelegram.Handlers.Users;

///<summary>
/// Returns basic user info according to their identifiers.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 FROM_MESSAGE_BOT_DISABLED Bots can't use fromMessage min constructors.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// See <a href="https://corefork.telegram.org/method/users.getUsers" />
///</summary>
internal sealed class GetUsersHandler : RpcResultObjectHandler<MyTelegram.Schema.Users.RequestGetUsers, TVector<MyTelegram.Schema.IUser>>,
    Users.IGetUsersHandler
{
    protected override Task<TVector<MyTelegram.Schema.IUser>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Users.RequestGetUsers obj)
    {
        throw new NotImplementedException();
    }
}
